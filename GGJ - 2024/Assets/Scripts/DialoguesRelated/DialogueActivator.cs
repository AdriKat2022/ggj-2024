using UnityEngine;


public class DialogueActivator : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField, Tooltip("Can the interaction be reused ?")]
    private bool repeatable;
    [SerializeField, Tooltip("The dialogue to display.")]
    private DialogueObject dialogueToDisplay;
    [SerializeField, Tooltip("The dialogue to display.")]
    private SubtitleObject subtitleToDisplay;

    [Header("Auto trigger")]
    [SerializeField, Tooltip("Disable the auto dialogue trigger.")]
    private bool usedByDialogueInteractible;


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
            if (dialogueToDisplay != null)
                dialogueHandler.ShowDialogue(dialogueToDisplay);
            if (subtitleToDisplay != null)
                dialogueHandler.ShowSubtitles(subtitleToDisplay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usedByDialogueInteractible)
            return;

        if (dialogueHandler != null)
        {
            TriggerDialogue();
        }
    }
}
