using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Vector2 Force;
    [SerializeField] BallController[] SpawnsOnPopping;

    Rigidbody2D rb;
    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            Animator a = GetComponent<Animator>();
            a.SetBool("ByWeapon", true);
            a.SetTrigger("Hit");
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Animator a = GetComponent<Animator>();
            a.SetBool("ByPlayer", true);
            a.SetTrigger("Hit");
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;
        }
    }

    public void BallDrops()
    {

    }
}
