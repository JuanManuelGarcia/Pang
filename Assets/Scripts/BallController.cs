using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IBallSubject, IStoppable
{
    [SerializeField] Vector2 Force;
    [SerializeField] GameObject SpawnOnPopping;

    List<IObserver> observers = new List<IObserver>();
    List<IBallSubject> ballsSpawned = new List<IBallSubject>();

    Rigidbody2D rb;
    Collider2D coll;
    Animator a;

    bool doDrops = false;
    Vector2 velocityBackup;

    public List<IBallSubject> BallsSpawned { get { return ballsSpawned; } }
    public bool IsStopped { get; private set; }

    void Start()
    {
        a = GetComponent<Animator>(); ;
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (doDrops)
        {
            if (SpawnOnPopping != null)
            {
                var go = GameObject.Instantiate(SpawnOnPopping);
                go.transform.SetParent(transform.root);
                go.transform.position = transform.position;
                var bc = go.GetComponent<BallController>();
                bc.Force = Force;
                ballsSpawned.Add(bc);

                go = GameObject.Instantiate(SpawnOnPopping);
                go.transform.SetParent(transform.root);
                go.transform.position = transform.position;
                bc = go.GetComponent<BallController>();
                bc.Force = Vector2.left * Force;
                ballsSpawned.Add(bc);
            }

            a.SetTrigger("SpawnComplete");
            doDrops = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;

            a.SetBool("ByWeapon", true);
            a.SetTrigger("Hit");
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;

            a.SetBool("ByPlayer", true);
            a.SetTrigger("Hit");
        }
    }

    public void BallDrops()
    {
        doDrops = true;
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
        foreach(IObserver o in observers)
        {
            o.Update(this);
        }
    }

    private void OnDestroy()
    {
        Notify();
        observers.Clear();
    }

    public void Stop()
    {
        velocityBackup = rb.velocity;
        rb.bodyType = RigidbodyType2D.Static;
        IsStopped = true;
    }

    public void Resume()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = velocityBackup;
        IsStopped = false;
    }
}
