using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Vector2 Force;
    [SerializeField] GameObject SpawnOnPopping;

    Rigidbody2D rb;
    Collider2D coll;
    Animator a;
    bool doDrops = false;

    // Start is called before the first frame update
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
                go.GetComponent<BallController>().Force = Force;

                go = GameObject.Instantiate(SpawnOnPopping);
                go.transform.SetParent(transform.root);
                go.transform.position = transform.position;
                go.GetComponent<BallController>().Force = Vector2.left * Force;
            }

            //TODO: Chance of powerup here!

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
}
