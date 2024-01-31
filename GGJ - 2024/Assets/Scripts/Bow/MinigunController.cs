using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunController : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject gunPrefab;
    [SerializeField] private Transform origin;
    private GameObject player;
    private bool isPickedUp;
    [SerializeField] private SubtitleObject holdMinigunDialog;
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
    

    // Update is called once per frame
    void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            if (blocked) return;

            if (!isPickedUp)
            {
                transform.parent = player.transform;
                transform.localPosition = new Vector3(1.3f, 0f, 0);
                isPickedUp = true;
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Destroy(FindObjectOfType<Arrow>().gameObject);
                DialogueHandler.Instance.ShowSubtitles(holdMinigunDialog, true);
            }
            else
            {
                Instantiate(gunPrefab, origin.position, Quaternion.identity);
                Instantiate(gunPrefab, origin.position, Quaternion.Euler(0,0,30));
                Instantiate(gunPrefab, origin.position, Quaternion.Euler(0, 0, -30));
            }
        }
        
    }
    private void BlockToggle(bool toggle)
    {
        blocked = toggle;
    }

}
