using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntry : MonoBehaviour
{
    private enum Dungeon{
        TreeTemple,
        MountTemple,
        SpaceTemple
    }

    [SerializeField , Range(1, 3)] private Dungeon dungeonId;


    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.Instance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player)) {


            switch (dungeonId)
            {
                case Dungeon.TreeTemple:

                    if (gameManager.dungeonState != 0)
                        return;
                    gameManager.dungeonState = 1;
                    SceneManager.LoadScene("TreeTemple");

                    break;

                case Dungeon.MountTemple:

                    if (gameManager.dungeonState != 2)
                        return;
                    gameManager.dungeonState = 3;
                    SceneManager.LoadScene("MountTemple");

                    break;

                case Dungeon.SpaceTemple:

                    if (gameManager.dungeonState != 4)
                        return;
                    gameManager.dungeonState = 5;
                    SceneManager.LoadScene("SpaceTemple");

                    break;
            }
        }
    }
}
