using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField] int HorizontalVelocity = 1;

    List<IPlayerObserver> observers = new List<IPlayerObserver>();
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
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
            foreach (IPlayerObserver o in observers)
            {
                o.PlayerDied(this);
            }
            observers.Clear();
        }
    }

    public void Attach(IPlayerObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IPlayerObserver observer)
    {
        observers.Remove(observer);
    }
}
