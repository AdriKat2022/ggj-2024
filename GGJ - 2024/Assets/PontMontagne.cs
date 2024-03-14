using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontMontagne : MonoBehaviour
{
    [SerializeField] private Collider2D montColl;
    [SerializeField] private GameObject dialGouffre;
    void Start()
    {
        if (GameManager.Instance.dungeonState < 3)
        {
            gameObject.SetActive(false);
            montColl.enabled = true;
            dialGouffre.SetActive(true);

        }
    }

}
