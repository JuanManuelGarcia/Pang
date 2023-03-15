using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerSubject
{
    [SerializeField] int HorizontalVelocity = 1;

    List<IObserver> observers = new List<IObserver>();
    Rigidbody2D rb;

    public bool IsDead { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        IsDead = false;
    }

    void Update()
    {
        rb.velocity = Vector2.zero;
        Vector2 force = Physics2D.gravity;

        force += new Vector2(HorizontalVelocity * Input.GetAxis("Horizontal"), 0);

        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")))
        {
            IsDead = true;
            Notify();
            observers.Clear();
        }
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IPlayerObserver o in observers)
        {
            o.Update(this);
        }
    }
}
