using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private GameObject player;
    private bool isPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPickedUp && collision.TryGetComponent(out PlayerMovement player))
        {
            this.player = player.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPickedUp && collision.TryGetComponent(out PlayerMovement _))
        {
            this.player = null;
        }
    }

    private void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0.3f, -0.18f, 0);
            isPickedUp = true;
            indicator.SetActive(false);
        }
    }
}
