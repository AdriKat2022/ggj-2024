using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ControllIndicator indicator;
    [SerializeField] private RabbitScript lapin;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float torqueForce;
    [SerializeField] private float launchDuration;

    [SerializeField] private Collider2D[] walls;
    [SerializeField] private DialogueObject endDialogue;

    private GameObject player;
    private bool isPickedUp;
    private bool isShooting;

    [SerializeField] private Sprite holdE;              // Échelle minimale
    [SerializeField] private Sprite pressE;              // Échelle minimale


    [SerializeField] private int bowBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPickedUp && collision.TryGetComponent(out PlayerMovement player))
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
                
            }
            if (!isShooting &&  anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                isShooting = true;
                transform.parent = null;
                if (bowBehaviour == 0)
                {
                    if(lapin) lapin.Flee();
                    StartCoroutine(Launch());
                }
                else if (bowBehaviour == 1)
                {
                    StartCoroutine(LaunchPlayer());
                }
                else if (bowBehaviour == 2)
                {
                    StartCoroutine(Launch());
                }

            }
        }
        else if(player && Input.GetKeyDown(KeyCode.E)) {
            transform.parent = player.transform;
            transform.localPosition = new Vector3(0.3f, -0.18f, 0);
            isPickedUp = true;
            indicator.ChangeSprite(holdE);
        }
        
    }

    private IEnumerator Launch()
    {
        float elapsedTime = 0f;
        Vector3 moveDirection = player.transform.right;


        while (elapsedTime < launchDuration)
        {
            // Déplace l'objet dans la direction spécifiée
            transform.Translate(launchSpeed * Time.deltaTime * moveDirection, Space.World);

            // Tourne l'objet constamment
            transform.Rotate(Time.deltaTime * torqueForce * Vector3.forward, Space.World);

            // Incrémente le temps écoulé
            elapsedTime += Time.deltaTime;

            // Attend la prochaine frame
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator LaunchPlayer()
    {
        float elapsedTime = 0f;
        Vector3 moveDirection = player.transform.right;


        while (elapsedTime < launchDuration)
        {
            player.transform.Translate(launchSpeed * Time.deltaTime * moveDirection, Space.World);

            player.transform.Rotate(Time.deltaTime * torqueForce * Vector3.forward, Space.World);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        player.transform.rotation = Quaternion.identity;
        indicator.ChangeSprite(pressE);
        isPickedUp = false;
        isShooting = false;
        bowBehaviour = 2;
        player = null;
    }

    public void InteractionEnd()
    {
        foreach (Collider2D wall in walls)
        {
            wall.enabled = false;
        }

        DialogueHandler.Instance.ShowDialogue(endDialogue, true);
    }

    private void OnDestroy()
    {
        InteractionEnd();
    }

}
