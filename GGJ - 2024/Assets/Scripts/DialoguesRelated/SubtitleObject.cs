using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Subtitle", order = 1)]
public class SubtitleObject : ScriptableObject
{
    [Serializable]
    public struct Subtitle
    {
        [TextArea]
        public string text;
        public float time;
        public float sizeModifer;
    }


    [field: Header("Subtitles")]
    [field: SerializeField, Tooltip("Can the user move during the bubbles ?")]
    public bool LockPlayerMovements { get; private set; }


    [field: Header("Subtitles")]
    [field: SerializeField]
    public Subtitle[] Bubbles { get; private set; }
    public int BubblesLength => Bubbles.Length;


    [field: Header("Follow up")]
    [field: SerializeField, Tooltip("Time after which the subtitle will switch.")]
    public float FollowUpTime { get; private set; }
    [field: SerializeField, Tooltip("Does the subtitle chain up on a dialogue ?\nIngored if there is a following subtitle.")]
    public DialogueObject FollowingDialogue { get; private set; }
    [field: SerializeField, Tooltip("Does the subtitle chain up on another one ?")]
    public SubtitleObject FollowingSubtitle { get; private set; }

}
