using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickCookie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cookieCounter;
    private int cookieCount;

    [SerializeField] private GameObject plusUnCookiePrefab;
    [SerializeField] private float shrinkFactor;
    [SerializeField] private float growFactor;
    [SerializeField] private float clicDuration;
    [SerializeField] private float clicIncreaseFactor;
    [SerializeField] private int cookieCountTransition;
    private bool isScaling = false;
    [SerializeField] private Vector3 originalScale;


    private void Start()
    {
        cookieCounter.text = "x0";
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !isScaling)
        {
            cookieCount++;
            cookieCounter.text = "x"+cookieCount.ToString();
            if(cookieCount == cookieCountTransition) SecondPhase();


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

    private void SecondPhase()
    {
        clicDuration /= clicIncreaseFactor;
    }
}
