using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerMovement blo;
    public EndDialogueAction endDialogueAction { get;  set; }
    public Vector3 cookieInitPos { get; set; }  

    private int dungeonState = 0;
    // 0 : aucun dongeon visité
    // 1 : premier dongeon visité
    // Etc...

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LoadDungeon(int dungeonId)
    {
        Debug.Log("Loading dungeon " + dungeonId);

        // TODO
    }

    public void executeEndDialogueAction()
    {
        if(endDialogueAction != null)
        {
            endDialogueAction.endAction();
            endDialogueAction=null;
        }
    }
}
