using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private GameObject bluePortal;
    [SerializeField] private GameObject orangePortal;
    [SerializeField] private float killTime;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(shootDirection * moveSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, killTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PortalBlock block))
        {
            if(block.GetColor())
            {
                Instantiate(bluePortal, collision.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(orangePortal, collision.transform.position, Quaternion.identity);
            }
            block.SetActive();
        }
    }
}
