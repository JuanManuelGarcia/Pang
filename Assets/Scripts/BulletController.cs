using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float Velocity = 750;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, Velocity));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Wall")) || 
            collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")) ||
            collision.gameObject.layer.Equals(LayerMask.NameToLayer("Container")))
        {
            Destroy(gameObject);
        }
    }
}
