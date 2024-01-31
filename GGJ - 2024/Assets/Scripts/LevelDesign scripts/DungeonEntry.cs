using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntry : MonoBehaviour
{
    [SerializeField , Range(1, 3)] private int dungeonId;





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player)) {
            if (dungeonId == 1 && GameManager.Instance.dungeonState == 0)
            {
                GameManager.Instance.dungeonState = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (dungeonId == 2 && GameManager.Instance.dungeonState == 2)
            {
                GameManager.Instance.dungeonState = 3;
                SceneManager.LoadScene("MountTemple");
            }
            else if (dungeonId == 3 && GameManager.Instance.dungeonState == 4)
            {
                GameManager.Instance.dungeonState = 5;
                SceneManager.LoadScene("SpaceTemple");
            }
        }
    }
}
