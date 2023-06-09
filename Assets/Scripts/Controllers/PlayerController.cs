using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerSubject, IPlayerPowerUps, IHourglassController
{
    [SerializeField] GameObject DefaultAmmoPrefab;
    [SerializeField] GameObject MachineGunAmmoPrefab;
    [SerializeField] float HorizontalVelocity = 1;

    public bool IsDead { get; private set; }
    public string CurrentWeaponName { get { return currentWeapon.Name; } }
    public int CurrentWeaponAmmo { get { return currentWeapon.Ammo; } }
    public IHourglassState HourglassState { get; set; }

    List<IObserver> observers = new List<IObserver>();
    Rigidbody2D rb;
    SpriteRenderer sr;

    IWeaponStrategy defaultWeapon;
    IWeaponStrategy currentWeapon;

    private bool shieldUp = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        defaultWeapon = new HookWeaponStrategy(DefaultAmmoPrefab);
        currentWeapon = defaultWeapon;
        Notify();

        IsDead = false;
    }

    void Update()
    {
        rb.velocity = Vector2.zero;
        Vector2 force = Physics2D.gravity;
        force += new Vector2(HorizontalVelocity * Screen.width * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
        rb.AddForce(force);

        if (Input.GetButton("Shoot"))
        {
            bool ammoLeft = (bool)currentWeapon.Execute(transform);

            if (!ammoLeft)
            {
                currentWeapon = defaultWeapon;
            }

            Notify();
        }

        if (HourglassState != null) HourglassState.Do();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")))
        {
            if (shieldUp)
            {
                ShieldDown();
            }
            else
            {
                IsDead = true;
                Notify();
                observers.Clear();
            }
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("PowerUp")))
        {
            collision.gameObject.GetComponent<IPowerUpStrategy>().Execute(this);
            PointsSingleton.Instance.AddPoints(1000);
            Destroy(collision.gameObject);
        }
    }

    private void ShieldDown()
    {
        shieldUp = false;
        sr.color = Color.white;
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver o in observers)
        {
            o.Revise(this);
        }
    }

    public void ApplyShield()
    {
        shieldUp = true;
        sr.color = Color.blue;
    }

    public void ApplyMachineGun()
    {
        if (currentWeapon is MachineGunWeaponStrategy)
        {
            currentWeapon.AddAmmo(MachineGunWeaponStrategy.InitialAmmo);
        }
        else
        {
            currentWeapon = new MachineGunWeaponStrategy(MachineGunAmmoPrefab);
        }
        Notify();
    }

    public void ApplyHourglass()
    {
        if (HourglassState is HourglassOnState) (HourglassState as HourglassOnState).ResetTime();
        else HourglassState = new HourglassOnState(this);
    }

    private void OnDestroy()
    {
        IsDead = true;
        Notify();
    }
}
