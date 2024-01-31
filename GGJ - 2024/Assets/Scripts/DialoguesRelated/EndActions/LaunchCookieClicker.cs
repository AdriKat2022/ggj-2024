using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCookieClicker : EndDialogueAction
{
    [SerializeField] private GameObject clicker;

    public override void endAction()
    {

        Vector3 spawnPos = Camera.main.transform.position;
        spawnPos.z = 0;


        GameManager.Instance.cookieInitPos = spawnPos;

        Instantiate(clicker);
    }

    private void Start()
    {
        if (GameManager.Instance.dungeonState == 2)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
