using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCookieClicker : EndDialogueAction
{
    [SerializeField] private GameObject clicker;

    public override void endAction()
    {
        Debug.Log("cookie spawn");

        Vector3 spawnPos = Camera.main.transform.position;
        spawnPos.y = 0;

        Instantiate(clicker, spawnPos, Quaternion.identity);
    }
}
