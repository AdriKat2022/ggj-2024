using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Slime : MonoBehaviour
{
    [SerializeField] private DialogueObject deathDialogue;
    [SerializeField] private Assecheriviere assecheRiv;
    [SerializeField] private float kickForce = 20;

    private Rigidbody2D rb;
    private Collider2D col;

    [SerializeField] private GameObject slime1;
    [SerializeField] private GameObject slime2;
    [SerializeField] private GameObject slime3;
    [SerializeField] private GameObject tuer;
    [SerializeField] private GameObject vivre;
    [SerializeField] private GameObject question;

    private bool punched = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    public void Dies()
    {

        StartCoroutine(DieCoroutine());
    }
    public IEnumerator Punch()
    {
        if (!punched)
        {
            punched = true;

            yield return new WaitForSecondsRealtime(1);

            yield return DialogueHandler.Instance.ShowDialogueWait(deathDialogue);
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.isTrigger = true;
            slime1.SetActive(true);
            slime2.SetActive(true);
            slime3.SetActive(true);
            
            question.SetActive(true);
            yield return new WaitForSecondsRealtime(7);

            tuer.SetActive(true);
            vivre.SetActive(true);
        }
    }

    private IEnumerator DieCoroutine()
    {
        slime1.SetActive(false);
        slime2.SetActive(false);
        slime3.SetActive(false);

        rb.AddForce(kickForce * (Vector2.down + Vector2.left).normalized, ForceMode2D.Impulse);
        rb.AddTorque(kickForce, ForceMode2D.Impulse);
        GameManager.Instance.dungeonState = 4;
        assecheRiv.enabled = true;

        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Overworld");
        Destroy(gameObject);
    }
}
