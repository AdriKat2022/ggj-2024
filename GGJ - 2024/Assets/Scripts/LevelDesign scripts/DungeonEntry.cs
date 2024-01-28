using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntry : MonoBehaviour
{
    [SerializeField , Range(1, 3)] private int dungeonId;

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.TryGetComponent(out PlayerController player))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    
}
