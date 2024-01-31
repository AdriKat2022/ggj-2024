using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuerSlime : MonoBehaviour
{
    [SerializeField] private Slime slime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out var _))
        {
            slime.Dies();
        }
    }
}
