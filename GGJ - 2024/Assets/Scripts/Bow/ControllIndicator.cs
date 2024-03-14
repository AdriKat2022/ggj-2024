using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllIndicator : MonoBehaviour
{
    [SerializeField] private float oscillationSpeed;     // Vitesse d'oscillation
    [SerializeField] private float maxHeight;             // Hauteur maximale
    [SerializeField] private float minHeight;             // Hauteur minimale
    [SerializeField] private float maxScale;              // Échelle maximale
    [SerializeField] private float minScale;              // Échelle minimale
    [SerializeField] private SpriteRenderer rend;              // Échelle minimale

    private void Update()
    {
        // Calcule une valeur sinusoidale pour l'oscillation
        float oscillationValue = Mathf.Sin(Time.time * oscillationSpeed);

        // Calcule la nouvelle position en fonction de la hauteur maximale et minimale
        Vector3 newPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(minHeight, maxHeight, (oscillationValue + 1f) / 2f), transform.localPosition.z);

        // Calcule la nouvelle échelle en fonction de l'échelle maximale et minimale
        float newScale = Mathf.Lerp(minScale, maxScale, (oscillationValue + 1f) / 2f);

        // Applique la nouvelle position et échelle à l'objet
        transform.localPosition = newPosition;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void ChangeSprite(Sprite sprite)
    {
        rend.sprite = sprite;
    }
}
