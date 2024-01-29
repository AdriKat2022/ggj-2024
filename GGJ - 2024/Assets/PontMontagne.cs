using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontMontagne : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance.dungeonState < 2)
        {
            gameObject.SetActive(false);
        }
    }

}
