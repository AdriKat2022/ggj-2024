using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitScript : MonoBehaviour
{
    [SerializeField] private Animator anim;     // Position de départ
    [SerializeField] private Vector3 startPosition;     // Position de départ
    [SerializeField] private Vector3 endPosition;       // Position d'arrivée
    [SerializeField] private float oscillationSpeed; // Vitesse d'oscillation en unités par seconde
    [SerializeField] private float fleeSpeed; // Vitesse d'oscillation en unités par seconde
    [SerializeField] private float destroyRabbitTime; // Vitesse d'oscillation en unités par seconde
    private bool isDead; // Vitesse d'oscillation en unités par seconde
    private bool willFlee; // Vitesse d'oscillation en unités par seconde
    [SerializeField] private SubtitleObject killRabbitSubtitle;
    [SerializeField] private float timeBeforeSwitchScene; // Vitesse d'oscillation en unités par seconde



    void Update()
    {
        if (isDead)
        {
        }
        else
        {
            if (willFlee)
            {
                startPosition += new Vector3(fleeSpeed * Time.deltaTime, 0, 0);
                endPosition += new Vector3(fleeSpeed * Time.deltaTime, 0, 0);
                
            }
            transform.position = startPosition + (endPosition - startPosition) * Mathf.PingPong(Time.time * oscillationSpeed, 1f);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead && collision.TryGetComponent(out Munition _)) {
            anim.SetBool("Dead", true);
            isDead = true;
            DialogueHandler.Instance.ShowSubtitles(killRabbitSubtitle, true);
        }
    }

    public void Flee()
    {
        willFlee = true;
        Destroy(gameObject, destroyRabbitTime);
    }

    public IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(timeBeforeSwitchScene);
        GameManager.Instance.dungeonState = 1;
        SceneManager.LoadScene("Hugo2");
    }
}
