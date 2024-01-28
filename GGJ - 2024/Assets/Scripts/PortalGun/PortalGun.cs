using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform origin;

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
            if(isPickedUp)
            {
                Instantiate(projectile, origin.position, Quaternion.identity);
            }
            transform.parent = player.transform;
            transform.localPosition = new Vector3(-0.76f, -0.14f, 0);
            isPickedUp = true;
        }
    }
}
