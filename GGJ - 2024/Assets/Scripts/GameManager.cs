using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
}
