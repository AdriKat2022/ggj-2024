using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickCookie : MonoBehaviour
{
    [SerializeField] private bool firstTime;
    [Header("Counter")]
    [SerializeField] private TextMeshProUGUI cookieCounter;
    private int cookieCount;
    [Header("ClicCookie")]

    [SerializeField] private GameObject plusUnCookiePrefab;
    [SerializeField] private float shrinkFactor;
    [SerializeField] private float growFactor;
    [SerializeField] private float clicDuration;
    [SerializeField] private float clicIncreaseFactor;
    [SerializeField] private int cookieCountTransition;
    private bool isScaling = false;
    [SerializeField] private Vector3 originalScale;

    [Header("Phase1")]
    [SerializeField] private GameObject speedLine;
    [SerializeField] private int cookieCountBreak;
    [SerializeField] private GameObject decorGauche;
    [SerializeField] private GameObject decorDroit;
    [SerializeField] private GameObject canvaCounter;
    [SerializeField] private GameObject surbrillance1;
    [SerializeField] private GameObject surbrillance2;
    [SerializeField] private float breakShrink;
    [SerializeField] private Collider2D colliderGouffre;
    [SerializeField] private SpriteRenderer spriteRendererPont;


    [Header("SecondTime")]
    [SerializeField] private GameObject main;
    [SerializeField] private float waitTimer;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float moveDuration;



    private void Start()
    {
        cookieCounter.text = "x0";
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !isScaling)
        {
            cookieCount++;
            if (cookieCount < cookieCountBreak + 6) cookieCounter.text = "x"+cookieCount.ToString();

            if(cookieCount >= cookieCountTransition)
            {
                if (firstTime) FirstTime();
                else StartCoroutine(SecondTime());
            }

            
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;

            // Instancie le prefab à la position du clic
            Instantiate(plusUnCookiePrefab, clickPosition, Quaternion.identity);
            StartCoroutine(ShrinkAndGrow());
        }
    }

    private IEnumerator ShrinkAndGrow()
    {
        // Évite la superposition d'effets en changeant l'état
        isScaling = true;

        // Rétrécissement
        float timer = 0f;
        while (timer < clicDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * shrinkFactor, timer / clicDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Croissance
        timer = 0f;
        while (timer < clicDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale * shrinkFactor, originalScale * growFactor, timer / clicDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < clicDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale * growFactor, originalScale, timer / clicDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        // Retour à la taille normale
        transform.localScale = originalScale;

        // Réinitialise l'état pour permettre d'autres clics
        isScaling = false;
    }

    private void FirstTime()
    {
        if (cookieCount == cookieCountTransition)
        {
            clicDuration /= clicIncreaseFactor;
            speedLine.SetActive(true);
        }

        if (cookieCount == cookieCountBreak)
        {
            StartCoroutine(decorGauche.GetComponent<BreakScript>().BreakSomething());
            speedLine.SetActive(false);
        }
        if (cookieCount == cookieCountBreak + 7) StartCoroutine(decorDroit.GetComponent<BreakScript>().BreakSomething());
        if (cookieCount == cookieCountBreak + 13)
        {
            StartCoroutine(gameObject.GetComponent<BreakScript>().BreakSomething());
            surbrillance1.SetActive(false);
            surbrillance2.SetActive(false);
            originalScale = originalScale * breakShrink;

        }
        if (cookieCount == cookieCountBreak + 18) canvaCounter.GetComponent<BreakUI>().BreakCounter();
        if (cookieCount == cookieCountBreak + 27)
        {
            StartCoroutine(gameObject.GetComponent<BreakCookie>().BreakEverything());
            StartCoroutine(decorGauche.GetComponent<BreakScript>().BreakSomethingTwo());
            StartCoroutine(decorDroit.GetComponent<BreakScript>().BreakSomethingTwo());
            canvaCounter.SetActive(false);
            colliderGouffre.enabled = false;
            spriteRendererPont.enabled = true;
        }


    }

    private IEnumerator SecondTime()
    {
        yield return new WaitForSeconds(waitTimer);
        canvaCounter.SetActive(false);

        float timer = 0f;
        Vector3 startPosition = transform.position;
        Vector3 startRotation = transform.rotation.eulerAngles;

        while (timer < moveDuration)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / moveDuration;
            main.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, t));
            main.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }
    }
}
