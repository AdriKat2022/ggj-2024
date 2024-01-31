using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Collider2D wallTree;
    [SerializeField] private SubtitleObject dialog0;
    [SerializeField] private SubtitleObject dialog1;
    [SerializeField] private SubtitleObject dialog2;

    private GameObject player;
    private bool isPickedUp;
    private bool isShooting;

    [SerializeField] private Sprite holdE;              // Échelle minimale
    [SerializeField] private Sprite pressE;              // Échelle minimale


    [SerializeField] private int bowBehaviour;

    [SerializeField] private GameObject minigun;
    private SoundManager soundM;

    private bool blocked;

    private void OnEnable()
    {
        DialogueHandler.OnDialogueOpenIsPlayerLocked += BlockToggle;
    }

    private void OnDisable()
    {
        DialogueHandler.OnDialogueOpenIsPlayerLocked -= BlockToggle;
    }


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

    private void Start()
    {
        soundM = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if(isPickedUp)
        {
            if (blocked || isShooting) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("Attack", true);
                anim.SetBool("Shoot", false);
                soundM.PlaySound(soundM.bowArrow);

            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Shoot", true);
                
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
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
                    if(wallTree) wallTree.enabled = false;
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
            if(bowBehaviour==1)
            {
                DialogueHandler.Instance.ShowSubtitles(dialog1, true);
            }
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
        if (bowBehaviour == 0) StartCoroutine(InteractionEnd());
        else if (bowBehaviour == 2) InteractionEnd2();
        Destroy(gameObject, 16f);
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

    public IEnumerator InteractionEnd()
    {
        DialogueHandler.Instance.ShowSubtitles(dialog0, true);
        yield return new WaitForSeconds(15);
        foreach (Collider2D wall in walls)
        {
            wall.enabled = false;
        }

    }

    public void InteractionEnd2()
    {

        DialogueHandler.Instance.ShowSubtitles(dialog2, true);
        Instantiate(minigun);
    }

    

    private void BlockToggle(bool toggle)
    {
        blocked = toggle;
    }

}
