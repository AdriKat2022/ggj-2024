using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntry : PlayerInteractable
{
    [SerializeField , Range(1, 3)] private int dungeonId;

    public override void Interact()
    {
        GameManager.Instance.LoadDungeon(dungeonId);
    }
}
