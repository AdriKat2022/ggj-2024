using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySwitchScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerMovement>(out var _))
        {
            GameManager.Instance.dungeonState = 2;
            StartCoroutine(LoadScene());
        }
        
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Overworld");
    }
}
