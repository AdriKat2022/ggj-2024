using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    [field: Header("Dialogue options")]
    [field: SerializeField, Tooltip("Characters per second")]
    public float DialogueSpeed { get; private set; } = 100;

    [field: SerializeField, Tooltip("Should punctuation wait be ignored ? (,?!. and - chars extends a bit the wait time after their display")]
    public bool IgnorePunctuation { get; private set; }

    [field: SerializeField, Tooltip("Can the user skip the bubbles ?")]
    public bool NonSkippable { get; private set; }

    [field: SerializeField, Tooltip("Should the user act to pass the bubble ? Or auto skip to immediately read the following bubble ?")]
    public bool AutoAdvance { get; private set; }

    [field: SerializeField, Tooltip("Time after which the dialogue will auto advance.")]
    public float AutoAdvanceTime { get; private set; }


    [field: Header("Dialogues")]
    [field: SerializeField, TextArea]
    public string[] Bubbles { get; private set; }
    public int BubblesLength => Bubbles.Length;


    [field: Header("Follow up")]
    [field: SerializeField, Tooltip("Does the dialogue chain up on another one ?\nUsually used to change to have different dialogue options in one go for multiple bubbles.")]
    public DialogueObject FollowingDialogue { get; private set; }

}
