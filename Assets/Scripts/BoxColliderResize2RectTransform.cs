using UnityEngine;

public class BoxColliderResize2RectTransform : MonoBehaviour
{
    void Awake()
    {
        RectTransform t = gameObject.GetComponent<RectTransform>();
        BoxCollider2D c = GetComponent<BoxCollider2D>();

        c.size = t.rect.size;
    }
}
