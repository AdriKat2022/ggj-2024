using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaisserVivre : MonoBehaviour
{
    [SerializeField] private Collider2D tuer;
    [SerializeField] private GameObject fragile;
    [SerializeField] private GameObject question;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out var _))
        {
            fragile.SetActive(true);
            question.SetActive(false);
            tuer.enabled = true;
        }
    }
}
