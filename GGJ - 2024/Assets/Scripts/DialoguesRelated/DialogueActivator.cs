using UnityEngine;


public class DialogueActivator : MonoBehaviour
{
    [SerializeField]
    private DialogueObject dialogueToDisplay;
    [SerializeField]
    private bool repeatable = false;

    [Space]

    [SerializeField]
    private DialogueHandler dialogueHandler;

    private bool activated = false;

    private void Start()
    {
        activated = false;
    }

    public void TriggerDialogue()
    {
        if (repeatable || !activated)
        {
            activated = true;
            dialogueHandler.ShowDialogue(dialogueToDisplay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogueToDisplay != null && dialogueHandler != null)
        {
            TriggerDialogue();
        }
    }
}
