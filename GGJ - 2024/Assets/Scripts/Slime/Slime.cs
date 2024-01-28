using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Slime : MonoBehaviour
{
    [SerializeField] private PostProcessVolume vol;
    [SerializeField] private DialogueObject deathDialogue;
    [SerializeField] private float kickForce = 20;

    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        SlimeDebug();
    }

    public void Dies()
    {
        vol.enabled = true;

        StartCoroutine(DieCoroutine());
    }


    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);

        yield return DialogueHandler.Instance.ShowDialogueWait(deathDialogue);

        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = true;

        rb.AddForce(kickForce * (Vector2.up + Vector2.left).normalized, ForceMode2D.Impulse);
        rb.AddTorque(kickForce, ForceMode2D.Impulse);

        vol.enabled = false;

        yield return new WaitForSecondsRealtime(1);

        Destroy(gameObject);
    }

    private void SlimeDebug()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Dies();
        }
    }
}
