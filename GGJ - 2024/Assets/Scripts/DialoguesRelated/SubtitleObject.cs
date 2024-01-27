using UnityEngine;

[CreateAssetMenu(menuName = "Subtitle", order = 1)]
public class SubtitleObject : ScriptableObject
{
    [field: Header("Subtitles")]
    [field: SerializeField, TextArea]
    public (string,float)[] Bubbles { get; private set; }
    public int BubblesLength => Bubbles.Length;


    [field: Header("Follow up")]
    [field: SerializeField, Tooltip("Does the dialogue chain up on another one ?\nUsually used to change to have different dialogue options in one go for multiple bubbles.")]
    public DialogueObject FollowingDialogue { get; private set; }

}
