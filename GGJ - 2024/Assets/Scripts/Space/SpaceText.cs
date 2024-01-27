using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // Assurez-vous d'attacher un composant TextMeshProUGUI à cet emplacement dans l'éditeur Unity

    [SerializeField] private float letterAppearDelay; // Délai entre chaque lettre
    [SerializeField] private float delayBefore; // Délai entre chaque lettre
    [SerializeField] private float delayAfter; // Délai entre chaque lettre
    [SerializeField] private GameObject cookieClicker; // Délai entre chaque lettre


    // Start is called before the first frame update
    void Start()
    {
        textComponent.maxVisibleCharacters = 0;
        StartCoroutine(ShowTextWithSound());

    }

    IEnumerator ShowTextWithSound()
    {
        yield return new WaitForSeconds(delayBefore);
        for (int i = 0; i < textComponent.text.Length; i++)
        {
            textComponent.maxVisibleCharacters = i + 1;
    
            yield return new WaitForSeconds(letterAppearDelay);
        }
        yield return new WaitForSeconds(delayAfter);
        textComponent.gameObject.SetActive(false);
        Instantiate(cookieClicker);
    }
}
