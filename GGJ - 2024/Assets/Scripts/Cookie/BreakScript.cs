using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    [Header("phase1")]
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float breakDuration;

    [Header("phase2")]
    [SerializeField] private Vector3 endPosition2;
    [SerializeField] private Vector3 endRotation2;
    [SerializeField] private float breakDuration2;

    private Vector3 intPos;

    private void Start()
    {
        intPos = GameManager.Instance.cookieInitPos;
    }
    public IEnumerator BreakSomething()
    {
        float timer = 0f;
        Vector3 startPosition = transform.position + intPos;
        Vector3 startRotation = transform.rotation.eulerAngles;

        while (timer < breakDuration)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / breakDuration;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, t));
            transform.position = Vector3.Lerp(startPosition + intPos, endPosition + intPos, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }
    }

    public IEnumerator BreakSomethingTwo()
    {
        float timer = 0f;
        Vector3 startPosition = transform.position + intPos;
        Vector3 startRotation = transform.rotation.eulerAngles;

        while (timer < breakDuration2)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / breakDuration2;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation2, t));
            transform.position = Vector3.Lerp(startPosition + intPos, endPosition2 + intPos, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }
    }
}
