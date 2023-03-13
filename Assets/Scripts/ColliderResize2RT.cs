using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResize2RT : MonoBehaviour
{
    void Start()
    {
        GetComponent<BoxCollider2D>().size = gameObject.GetComponent<RectTransform>().rect.size;
    }
}
