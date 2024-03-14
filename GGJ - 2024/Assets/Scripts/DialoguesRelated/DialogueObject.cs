using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    [field: Header("Text timing")]
    [field: SerializeField, Tooltip("Characters per second")]
    public float DialogueSpeed { get; private set; } = 100;

    [field: SerializeField, Tooltip("Should punctuation wait be ignored ? (,?!. and - chars extends a bit the wait time after their display")]
    public bool IgnorePunctuation { get; private set; }

    [field: SerializeField, Tooltip("If true, this will not count white spaces as characters to wait")]
    public bool IgnoreWhiteSpaces { get; private set; }

    [field: Header("Inputs")]

    [field: SerializeField, Tooltip("Can the user move during the bubbles ?")]
    public bool LockPlayerMovements { get; private set; }

    [field: SerializeField, Tooltip("Can the user skip the bubbles ?")]
    public bool NonSkippable { get; private set; }

    [field: SerializeField, Tooltip("Should the user press a button to go to the next bubble ? Or auto skips to immediately read the following bubble ?")]
    public bool AutoAdvance { get; private set; }

    [field: SerializeField, Tooltip("Time after which the dialogue will auto advance if AutoAdvance is set to true.")]
    public float AutoAdvanceTime { get; private set; }


    [field: Header("Dialogues")]
    [field: SerializeField, TextArea]
    public string[] Bubbles { get; private set; }
    public int BubblesLength => Bubbles.Length;

    [Header("Events")]
    public UnityEvent DialogueEvent;
    public int DialogueEventIndex;


    [field: Header("Follow up")]
    [field: SerializeField, Tooltip("Time after which the dialogue will switch.")]
    public float FollowUpTime { get; private set; }
    [field: SerializeField, Tooltip("Does the dialogue chain up on another one ?\nUsually used to change to have different dialogue options in one go for multiple bubbles.")]
    public DialogueObject FollowingDialogue { get; private set; }
    [field: SerializeField, Tooltip("Does the dialogue chain up on a subtitle ?\nIngored if there is a following dialogue.")]
    public SubtitleObject FollowingSubtitle { get; private set; }

}
