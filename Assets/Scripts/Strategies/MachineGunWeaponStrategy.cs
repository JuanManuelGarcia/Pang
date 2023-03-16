using UnityEngine;

public class MachineGunWeaponStrategy : IWeaponStrategy
{
    public const int InitialAmmo = 50;
    private const float Cooldown = 0.1f;

    public string Name { get { return "Machine Gun"; } }
    public int Ammo { get; private set; }

    private GameObject ammoPrefab;
    private float timeLastBulletFired;

    public MachineGunWeaponStrategy(GameObject ammoPrefab)
    {
        timeLastBulletFired = float.NegativeInfinity;
        this.ammoPrefab = ammoPrefab;
        Ammo = InitialAmmo;
    }

    public void AddAmmo(int extraAmmo)
    {
        Ammo += extraAmmo;
    }

    public object Execute(object data) // In: Transform; Out: bool
    {
        if (Ammo > 0 && (Time.time - timeLastBulletFired) > Cooldown)
        {
            Transform playerTransform = (Transform)data;

            GameObject go = GameObject.Instantiate(ammoPrefab);
            go.transform.position = playerTransform.position;
            go.transform.parent = playerTransform;

            timeLastBulletFired = Time.time;
            Ammo--;
        }

        return Ammo > 0; // return false if no more ammo, true otherwise
    }
}
