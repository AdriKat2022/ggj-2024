using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ControllIndicator indicator;
    private GameObject player;
    private bool isPickedUp;

    private int bowBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement player))
        {
            this.player = player.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement _))
        {
            this.player = null;
        }
    }

    private void Update()
    {
        if(isPickedUp)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Shoot", false);
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Shoot", true);
                if(bowBehaviour==0)
                {
                    bowBehaviour = 1;
                }
            }
        }
        else if(player && Input.GetKeyDown(KeyCode.E)) {
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0.3f, -0.18f, 0);
            isPickedUp = true;
            indicator.ChangeSprite();
        }
        
    }
}
