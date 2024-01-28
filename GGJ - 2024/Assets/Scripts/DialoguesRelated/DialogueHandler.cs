using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public enum EventType
    {
        All,
        Dialogue,
        Subtitle
    }


    [Header("Animations")]
    [SerializeField]
    private float bubbleAnimationTime;
    [SerializeField]
    private float readyAnimationSpeed;
    [SerializeField]
    private float readyAnimationDepth;
    [SerializeField]
    private float bubbleAnimationTimeSubtitle;
    [SerializeField]
    private float readyAnimationSpeedSubtitle;
    [SerializeField]
    private float readyAnimationDepthSubtitle;

    [Header("Dialogue")]
    [SerializeField]
    private TMP_Text dialogueTextLabel;
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private GameObject readyIcon;

    [Header("Subtitles")]
    [SerializeField]
    private TMP_Text subtitleTextLabel;
    [SerializeField]
    private GameObject subtitleBox;

    public bool IsOpen { get; private set; }

    [SerializeField]
    private TypeWritterEffect typeWritter;
    [SerializeField]
    private InstantWritterEffect instantWritter;

    private Vector2 readyIconBasePosition;
    private bool isReadyToAdvance;
    private DialogueObject currentDialogue;
    private SubtitleObject currentSubtitles;
    private float subtitlesBaseFontSize;
    //private float dialoguesBaseFontSize;


    [Header("Debug")]
    [SerializeField]
    private SubtitleObject testSubtitles;
    [SerializeField]
    private DialogueObject testDialogue;

    #region Events

    public static event Action<bool> OnDialogueOpenIsPlayerLocked;

    #endregion


    #region Singleton

    public static DialogueHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion


    private void Start()
    {
        if (dialogueBox == null)
            Debug.LogError("Dialogue box is not assigned.\n", gameObject);

        //if (!TryGetComponent(out typeWritter))
        //    Debug.LogError("No TypeEffect component was found.\nYou need an effect to display text.", gameObject);

        subtitlesBaseFontSize = subtitleTextLabel.fontSize;

        IsOpen = false;
        isReadyToAdvance = false;
        readyIcon.SetActive(false);
        readyIconBasePosition = readyIcon.transform.position;
        CloseDialogueBox();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            ShowDialogue(testDialogue);
        if (Input.GetKeyDown(KeyCode.C))
            ShowSubtitles(testSubtitles);
    }


    #region Public functions

    /// <summary>
    /// This will make the dialogue box appear and display all the bubbles and following dialogues.
    /// </summary>
    /// <param name="dialogueObjectToDisplay">The dialogue object you want to display</param>
    public void ShowDialogue(DialogueObject dialogueObjectToDisplay, bool forceOpen = false)
    {
        if (IsOpen && !forceOpen)
            return;

        IsOpen = true;
        currentDialogue = dialogueObjectToDisplay;
        StartCoroutine(StartDialogueAnimation());
    }
    /// <summary>
    /// This will make the dialogue box appear and display all the bubbles and following dialogues.
    /// </summary>
    /// <param name="dialogueObjectToDisplay">The dialogue object you want to display</param>
    public IEnumerator ShowDialogueWait(DialogueObject dialogueObjectToDisplay, bool forceOpen = false)
    {
        if (IsOpen && !forceOpen)
            yield break;

        IsOpen = true;
        currentDialogue = dialogueObjectToDisplay;
        yield return StartDialogueAnimation();
    }
    /// <summary>
    /// This will make the subtitles box appear and display all the bubbles and following subtitles.
    /// </summary>
    /// <param name="subtitlesToDisplay">The dialogue object you want to display as subtitles</param>
    public void ShowSubtitles(SubtitleObject subtitlesToDisplay, bool forceOpen = false)
    {
        if (IsOpen && !forceOpen)
            return;

        IsOpen = true;
        currentSubtitles = subtitlesToDisplay;
        StartSubtitlesAnimation();
    }

    public void Interrupt(EventType eventToInterrupt)
    {
        switch (eventToInterrupt)
        {
            case EventType.All:
                StopAllCoroutines();
                dialogueTextLabel.text = string.Empty;
                dialogueBox.SetActive(false);
                subtitleTextLabel.text = string.Empty;
                subtitleBox.SetActive(false);
                IsOpen = false;
                OnDialogueOpenIsPlayerLocked?.Invoke(false);
                break;

            case EventType.Dialogue:

                // nah

                break;

            case EventType.Subtitle:

                // nah

                break;
        }
    }

    #endregion

    #region Dialogues

    private IEnumerator StartDialogueAnimation()
    {
        OnDialogueOpenIsPlayerLocked?.Invoke(currentDialogue.LockPlayerMovements);

        float scale = 0f;

        dialogueBox.transform.localScale = new Vector3(1, scale, 1);
        dialogueBox.SetActive(true);

        while (scale < 1f)
        {
            scale = Mathf.Min(1f, scale + Time.deltaTime / bubbleAnimationTime);
            dialogueBox.transform.localScale = new Vector3(1, scale, 1);

            yield return null;
        }

        yield return StepThroughDialogue();
    }

    private IEnumerator StepThroughDialogue()
    {
        OnDialogueOpenIsPlayerLocked?.Invoke(currentDialogue.LockPlayerMovements);

        for(int i = 0 ; i<currentDialogue.BubblesLength ; i++)
        {
            yield return RunTypingEffect(i);

            dialogueTextLabel.text = currentDialogue.Bubbles[i];

            yield return null;

            if (!currentDialogue.AutoAdvance)
            {
                StartCoroutine(ReadyAnimation());
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
            else
                yield return new WaitForSeconds(currentDialogue.AutoAdvanceTime);

            //SoundManager.Instance.PlaySound(SoundManager.Instance.button_press);

            isReadyToAdvance = false;
        }

        if (currentDialogue.FollowingDialogue != null)
        {
            currentDialogue = currentDialogue.FollowingDialogue;
            yield return StepThroughDialogue();
            yield break;
        }

        StartCoroutine(StopDialogueAnimation());

        if (currentDialogue.FollowingSubtitle != null)
        {
            yield return new WaitForSeconds(currentDialogue.FollowUpTime);
            ShowSubtitles(currentDialogue.FollowingSubtitle, true);
            yield break;
        }
        
        OnDialogueOpenIsPlayerLocked?.Invoke(false);
    }

    private IEnumerator StopDialogueAnimation()
    {
        ResetDialogueBox();

        float scale = 1f;

        dialogueBox.transform.localScale = new Vector3(1, scale, 1);

        while (scale > 0f)
        {
            scale = Mathf.Max(0f, scale - Time.deltaTime / bubbleAnimationTime);
            dialogueBox.transform.localScale = new Vector3(1, scale, 1);

            yield return null;
        }

        CloseDialogueBox();
    }

    private IEnumerator RunTypingEffect(int index)
    {
        typeWritter.RunDialogue(currentDialogue, index, dialogueTextLabel);

        yield return null;

        while (typeWritter.IsRunning)
        {
            if (!currentDialogue.NonSkippable && Input.GetKeyDown(KeyCode.E))
                typeWritter.StopDialogue();

            yield return null;
        }
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        ResetDialogueBox();
    }

    private void ResetDialogueBox()
    {
        dialogueTextLabel.text = string.Empty;
        //dialogueTitleLabel.text = string.Empty;
    }

    #endregion 

    #region Subtitles

    private void StartSubtitlesAnimation()
    {
        subtitleBox.SetActive(true);
        StartCoroutine(StepThroughSubtitles());
    }

    private IEnumerator StepThroughSubtitles()
    {
        OnDialogueOpenIsPlayerLocked?.Invoke(currentSubtitles.LockPlayerMovements);

        int i = 0;

        while (i < currentSubtitles.BubblesLength)
        {
            subtitleTextLabel.fontSize = subtitlesBaseFontSize + currentSubtitles.Bubbles[i].sizeModifer;
            instantWritter.RunDialogue(currentSubtitles, i, subtitleTextLabel);
            yield return new WaitForSeconds(currentSubtitles.Bubbles[i].time);
            i++;
        }

        if (currentSubtitles.FollowingSubtitle != null)
        {
            yield return new WaitForSeconds(currentSubtitles.FollowUpTime);
            ShowSubtitles(currentSubtitles.FollowingSubtitle, true);
            yield break;
        }

        CloseSubtitlesBox();

        if (currentSubtitles.FollowingDialogue != null)
        {
            yield return new WaitForSeconds(currentSubtitles.FollowUpTime);
            ShowDialogue(currentSubtitles.FollowingDialogue, true);
        }
        
        OnDialogueOpenIsPlayerLocked?.Invoke(false);
    }

    private void CloseSubtitlesBox()
    {
        subtitleTextLabel.text = string.Empty;
        subtitleBox.SetActive(false);
    }

    #endregion


    private IEnumerator ReadyAnimation()
    {
        isReadyToAdvance = true;
        float time = 0f;

        readyIcon.SetActive(true);

        while (true)
        {
            readyIcon.transform.position = readyIconBasePosition + Mathf.Abs(Mathf.Sin(time * readyAnimationSpeed)) * readyAnimationDepth * Vector2.up;

            time += Time.deltaTime;

            if (!isReadyToAdvance)
            {
                readyIcon.SetActive(false);
                yield break;
            }

            yield return null;
        }
    }
}
