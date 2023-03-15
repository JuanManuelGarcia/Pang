using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Vector2 Force;
    [SerializeField] GameObject SpawnOnPopping;

    Rigidbody2D rb;
    Collider2D coll;
    bool doDrops = false;
    Vector2 velocityOnHit;

    // Start is called before the first frame update
    void Start()
    {
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
                go.transform.SetParent(transform.parent);
                go.GetComponent<BallController>().Force = velocityOnHit;
                go = GameObject.Instantiate(SpawnOnPopping);
                go.transform.SetParent(transform.parent);
                go.GetComponent<BallController>().Force = Vector2.left * velocityOnHit;
            }

            //TODO: Chance of powerup here!

            doDrops = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            velocityOnHit = rb.velocity;
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;

            Animator a = GetComponent<Animator>();
            a.SetBool("ByWeapon", true);
            a.SetTrigger("Hit");
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            velocityOnHit = rb.velocity;
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;

            Animator a = GetComponent<Animator>();
            a.SetBool("ByPlayer", true);
            a.SetTrigger("Hit");
        }
    }

    public void BallDrops()
    {
        Debug.Log("b");
        doDrops = true;
    }
}
