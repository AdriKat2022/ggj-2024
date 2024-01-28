using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBlock : MonoBehaviour
{
    [SerializeField] private PortalBlock linkPortal;
    [SerializeField] private bool blueColor;
    private bool isActive;

    public bool GetColor()
    {
        return blueColor;
    }

    public void SetActive()
    {
        isActive = true;
    }

    public bool GetActive()
    {
        return isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isActive && linkPortal.GetActive() && collision.TryGetComponent(out PlayerMovement player))
        {
            SceneManager.LoadScene("SpaceScene");
        }
    }
}
