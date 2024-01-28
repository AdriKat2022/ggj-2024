using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TypeWritterEffect : MonoBehaviour
{
    private DialogueObject currentDialogue;
    private Coroutine typingCoroutine;

    public bool IsRunning { get; private set; }


    private readonly Dictionary<HashSet<char>, float> punctuationWaitTimeChart = new()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.6f },
        {new HashSet<char>(){',', ';', ':'}, 0.3f }
    };


    public void RunDialogue(DialogueObject dialogue, int index, TMP_Text bubbleText)
    {
        if(dialogue == null)
        {
            Debug.LogError("Internal error : Null dialogue passed");
            return;
        }
        if (dialogue.BubblesLength <= index)
        {
            Debug.LogError("Internal error : Out of range dialogue requested");
            return;
        }
        currentDialogue = dialogue;
        typingCoroutine = StartCoroutine(TypeText(dialogue.Bubbles[index], bubbleText));
    }

    public void StopDialogue()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text bubbleText)
    {
        IsRunning = true;

        float time = 0;
        int currentCharIndex = 0;
        int lastWait = -1;

        while (currentCharIndex < textToType.Length - 1)
        {
            int lastCharIndex = currentCharIndex;

            time += Time.deltaTime * currentDialogue.DialogueSpeed;

            if (Time.deltaTime * currentDialogue.DialogueSpeed > 1)
                Debug.LogWarning("Text is too fast, it might not account all wait times for repeated punctuation.");

            currentCharIndex = Mathf.FloorToInt(time);
            currentCharIndex = Mathf.Clamp(currentCharIndex, 0, textToType.Length-1);
            
            while (currentDialogue.IgnoreWhiteSpaces && textToType[currentCharIndex] == ' ' && currentCharIndex < textToType.Length-1)
            {
                currentCharIndex++;
                time = currentCharIndex;
            }


            // The for loop is just for when there is multiple characters that have to show up at the same frame
            for (int i = lastCharIndex; i <= currentCharIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                bubbleText.text = textToType[..(i+1)];

                //SoundManager.Instance.PlaySound(SoundManager.Instance.text_char);

                // This code checks if we're not skipping waitable chars
                if (i>lastWait && CheckPunctuation(textToType[i], out float waitTime) && !isLast)
                {
                    //Debug.Log("punct detected ("+i+")\nWait time : "+waitTime);
                    //Debug.Log(currentCharIndex);
                    //Debug.Log(textToType.Length - 1);

                    time = i;
                    lastWait = i;
                    currentCharIndex = i;
                    yield return new WaitForSeconds(waitTime);
                    break;
                }
            }

            yield return null;
        }

        IsRunning = false;
    }

    private bool CheckPunctuation(char character, out float waitTime)
    {
        waitTime = default;

        if (currentDialogue.IgnorePunctuation)
            return false;

        foreach (KeyValuePair<HashSet<char>, float> punctuationCategory in punctuationWaitTimeChart)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }


        return false;
    }
}
