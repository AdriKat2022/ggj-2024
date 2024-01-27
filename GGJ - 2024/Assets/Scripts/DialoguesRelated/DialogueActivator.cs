using UnityEngine;


public class DialogueActivator : MonoBehaviour
{
    [SerializeField]
    private DialogueObject dialogueToDisplay;
    [SerializeField]
    private bool repeatable = false;

    [Space]

    private DialogueHandler dialogueHandler;

    private bool activated = false;

    private void Start()
    {
        dialogueHandler = DialogueHandler.Instance;
        if (dialogueHandler == null)
            Debug.LogError("There is no dialogue handler in the scene. Dialogues won't open.");

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
