using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Slime : MonoBehaviour
{
    [SerializeField] private PostProcessVolume vol;
    [SerializeField] private DialogueObject deathDialogue;

    private void Update()
    {
        SlimeDebug();
    }

    public void Dies()
    {
        Debug.Log("dies");
        vol.enabled = true;

        StartCoroutine(DieCoroutine());
    }


    IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);

        DialogueHandler.Instance.ShowDialogue(deathDialogue);
    }

    private void SlimeDebug()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Dies();
        }
    }
}
