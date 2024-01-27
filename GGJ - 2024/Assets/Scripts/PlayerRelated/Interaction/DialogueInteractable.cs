using UnityEngine;

public class DialogueInteractable : PlayerInteractable
{
    [Header("Dialogue Interactable")]
    [SerializeField, Tooltip("The dialogue Activator to trigger.")]
    private DialogueActivator dialogueActivator;

    public override void Interact()
    {
        dialogueActivator.TriggerDialogue();
    }

    protected override void OnInteractable()
    {

    }
}
