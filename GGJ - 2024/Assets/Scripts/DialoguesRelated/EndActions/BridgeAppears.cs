using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAppears : EndDialogueAction
{
    [SerializeField] private GameObject pont;
    [SerializeField] private Collider2D removedCollider;

    public override void endAction()
    {
        pont.SetActive(true);
        removedCollider.enabled = false;
    }
}
