using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] float GrowthVelocity;
    [SerializeField] float InitialScaleY = 0.01f;

    Rigidbody2D rb;
    bool fallingToGrownd = true;
    float totalGrowth;
    float groundY;

    void Start()
    {
        totalGrowth = InitialScaleY;
        transform.localScale = new Vector3(transform.localScale.x, totalGrowth, transform.localScale.z);

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Grow(GrowthVelocity * Time.deltaTime);
    }

    private void Grow(float growth)
    {
        totalGrowth += growth;
        if (!fallingToGrownd) transform.position = new Vector3(transform.position.x, groundY + (totalGrowth / 2), transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x, totalGrowth, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Wall")))
        {
            if (fallingToGrownd)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                groundY = transform.position.y - totalGrowth / 2;
                fallingToGrownd = false;
            }
            else Destroy(gameObject);
        }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")) ||
            collision.gameObject.layer.Equals(LayerMask.NameToLayer("Container")))
        {
            Destroy(gameObject);
        }
    }
}
