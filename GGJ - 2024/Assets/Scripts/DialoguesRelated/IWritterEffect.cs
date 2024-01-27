using TMPro;

public interface IWritterEffect
{
    public bool IsRunning { get; }

    public void StopDialogue();
    public void RunDialogue(DialogueObject dialogueObject, int index, TMP_Text textPlaceHolder);
}
