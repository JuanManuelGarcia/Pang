using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [SerializeField] GameObject PowerUpPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Weapon")))
        {
            var go = Instantiate(PowerUpPrefab);
            go.transform.position = transform.position;
            go.transform.parent = transform.root;

            Destroy(gameObject);
        }
    }
}
