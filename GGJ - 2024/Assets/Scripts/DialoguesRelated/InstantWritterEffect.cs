using TMPro;
using UnityEngine;

public class InstantWritterEffect : MonoBehaviour
{

    public void RunDialogue(SubtitleObject dialogueObject, int index, TMP_Text textPlaceHolder)
    {
        textPlaceHolder.text = dialogueObject.Bubbles[index].text;
    }
}
