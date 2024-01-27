using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickCookie : MonoBehaviour
{
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

    [Header("Phase2")]
    [SerializeField] private GameObject speedLine;
    [SerializeField] private int cookieCountBreak;
    [SerializeField] private GameObject decorGauche;
    [SerializeField] private GameObject decorDroit;
    [SerializeField] private GameObject canvaCounter;
    [SerializeField] private GameObject surbrillance1;
    [SerializeField] private GameObject surbrillance2;
    [SerializeField] private float breakShrink;



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
            if(cookieCount == cookieCountTransition) SecondPhase();

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
            }


            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;

            // Instancie le prefab � la position du clic
            Instantiate(plusUnCookiePrefab, clickPosition, Quaternion.identity);
            StartCoroutine(ShrinkAndGrow());
        }
    }

    private IEnumerator ShrinkAndGrow()
    {
        // �vite la superposition d'effets en changeant l'�tat
        isScaling = true;

        // R�tr�cissement
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
        // Retour � la taille normale
        transform.localScale = originalScale;

        // R�initialise l'�tat pour permettre d'autres clics
        isScaling = false;
    }

    private void SecondPhase()
    {
        clicDuration /= clicIncreaseFactor;
        speedLine.SetActive(true);
    }
}