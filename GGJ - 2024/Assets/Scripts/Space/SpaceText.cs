using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent; // Assurez-vous d'attacher un composant TextMeshProUGUI � cet emplacement dans l'�diteur Unity

    [SerializeField] private float letterAppearDelay; // D�lai entre chaque lettre
    [SerializeField] private float delayBefore; // D�lai entre chaque lettre
    [SerializeField] private float delayAfter; // D�lai entre chaque lettre
    [SerializeField] private float delayBeforeCookieClicker; // D�lai entre chaque lettre
    [SerializeField] private GameObject cookieClicker; // D�lai entre chaque lettre
    [SerializeField] private SoundManager soundM;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.maxVisibleCharacters = 0;
        StartCoroutine(ShowTextWithSound());
        soundM.PlayMusic(soundM.spaceTheme);

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
        yield return new WaitForSeconds(delayBeforeCookieClicker);
        Instantiate(cookieClicker);
        soundM.StopMusic();
    }
}
