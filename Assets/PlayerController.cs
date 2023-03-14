using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int HorizontalVelocity = 1;

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
            Debug.Log("ouch");
        }
    }
}
