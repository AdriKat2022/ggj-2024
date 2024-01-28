using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothApparition : MonoBehaviour
{
    [SerializeField] private float startDuration;  // Durée de la transition en secondes
    [SerializeField] private float transitionDuration;  // Durée de la transition en secondes
    private Vector3 intPos;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 intermediateRotation;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 intermediatePosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 startScale;
    [SerializeField] private Vector3 intermediateScale;
    [SerializeField] private Vector3 endScale;

    void Start()
    {
        intPos = GameManager.Instance.cookieInitPos;
        // Démarre la coroutine pour la transition
        StartCoroutine(TransitionRoutine());
    }

    IEnumerator TransitionRoutine()
    {
        float timer = 0f;

        yield return new WaitForSeconds(startDuration);
        while (timer < transitionDuration)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / transitionDuration;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, intermediateRotation, t));
            transform.position = Vector3.Lerp(intPos + startPosition, intPos + intermediatePosition, t);
            transform.localScale = Vector3.Lerp(startScale, intermediateScale, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }

        timer = 0f;

        while (timer < transitionDuration)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / transitionDuration;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(intermediateRotation, endRotation, t));
            transform.position = Vector3.Lerp(intPos + intermediatePosition, intPos + endPosition, t);
            transform.localScale = Vector3.Lerp(intermediateScale, endScale, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }

        // Assure que la transition est complète en fixant les valeurs finales
        transform.rotation = Quaternion.Euler(endRotation);
        transform.position = endPosition + intPos;
        transform.localScale = endScale;

    }
}
