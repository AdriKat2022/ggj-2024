using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonArrow : MonoBehaviour
{
    private bool activate;
    [SerializeField] private GameObject arrow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activate && collision.TryGetComponent(out PlayerMovement _)) { 
            activate = true;
            Instantiate(arrow, transform.position, Quaternion.identity);
        }
    }
}
