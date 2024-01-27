using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TypeWritterEffect : MonoBehaviour, IWritterEffect
{
    private DialogueObject currentDialogue;
    private Coroutine typingCoroutine;

    public bool IsRunning { get; private set; }


    private readonly Dictionary<HashSet<char>, float> punctuationWaitTimeChart = new()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.6f },
        {new HashSet<char>(){',', ';', '-', ':'}, 0.3f }
    };


    public void RunDialogue(DialogueObject dialogue, int index, TMP_Text bubbleText)
    {
        if(dialogue == null)
        {
            Debug.LogError("Null dialogue passed");
            return;
        }
        if (dialogue.BubblesLength <= index)
        {
            Debug.LogError("Out of range dialogue requested");
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

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * currentDialogue.DialogueSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            // The for loop is just for when there is multiple characters that have to show up at the same frame
            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                bubbleText.text = textToType[..(i + 1)];

                //SoundManager.Instance.PlaySound(SoundManager.Instance.text_char);

                if (CheckPunctuation(textToType[i], out float waitTime) && !isLast)
                {
                    if (CheckPunctuation(textToType[i + 1], out float secondWaitTime))
                    {
                        waitTime /= 2;
                    }

                    yield return new WaitForSeconds(waitTime);
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
