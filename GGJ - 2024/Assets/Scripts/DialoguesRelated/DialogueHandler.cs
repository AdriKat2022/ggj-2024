using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField]
    private float bubbleAnimationTime;
    [SerializeField]
    private float readyAnimationSpeed;
    [SerializeField]
    private float readyAnimationDepth;

    [Header("References")]
    [SerializeField]
    private TMP_Text dialogueTextLabel;
    [SerializeField]
    private TMP_Text dialogueTitleLabel;
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private GameObject readyIcon;

    public bool IsOpen { get; private set; }

    private TypeWritterEffect typeWritter;

    private Vector2 readyIconBasePosition;
    private bool isReadyToAdvance;
    private DialogueObject currentDialogue;


    private void Start()
    {
        if (dialogueBox == null || !dialogueBox.TryGetComponent(out typeWritter))
            Debug.LogError("Dialogue box is not assigned or no TypeEffect component was found on it.\nYou need a dialogue game object containing", gameObject);
        

        IsOpen = false;
        isReadyToAdvance = false;
        readyIcon.SetActive(false);
        readyIconBasePosition = readyIcon.transform.position;
        CloseDialogueBox();
    }

    #region Dialogues

    /// <summary>
    /// This will make the dialogue box appear and display all the bubbles and following dialogues.
    /// </summary>
    /// <param name="dialogueObjectToDisplay">The dialogue object you want to display</param>
    public void ShowDialogue(DialogueObject dialogueObjectToDisplay)
    {
        IsOpen = true;
        currentDialogue = dialogueObjectToDisplay;
        StartCoroutine(StartDialogueAnimation());
    }

    private IEnumerator StartDialogueAnimation()
    {
        float scale = 0f;

        dialogueBox.transform.localScale = new Vector3(1, scale, 1);
        dialogueBox.SetActive(true);

        while (scale < 1f)
        {
            scale = Mathf.Min(1f, scale + Time.deltaTime / bubbleAnimationTime);
            dialogueBox.transform.localScale = new Vector3(1, scale, 1);

            yield return null;
        }

        StartCoroutine(StepThroughDialogue());
    }

    private IEnumerator StepThroughDialogue()
    {
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

            yield return new WaitForSeconds(currentDialogue.AutoAdvanceTime);

            //SoundManager.Instance.PlaySound(SoundManager.Instance.button_press);

            isReadyToAdvance = false;
        }

        StartCoroutine(StopDialogueAnimation());
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
        typeWritter.RunDialog(currentDialogue, index, dialogueTextLabel);

        while (typeWritter.IsRunning)
        {
            if (currentDialogue.Skippable && Input.GetKeyDown(KeyCode.E))
            {
                typeWritter.Stop();
            }

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
        dialogueTitleLabel.text = string.Empty;
    }


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

    #endregion
}
