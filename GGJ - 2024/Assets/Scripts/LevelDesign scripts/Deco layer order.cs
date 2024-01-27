using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decolayerorder : MonoBehaviour
{
    [SerializeField] private int layerFactor = 10;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = - layerFactor * (int) transform.position.y;
    }
}
