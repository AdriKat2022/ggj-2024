using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTP : MonoBehaviour
{
    [SerializeField] GameObject go;

    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            collision.gameObject.transform.position = go.transform.position;
        }
    }
}
