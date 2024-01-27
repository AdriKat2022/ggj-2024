using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSpace : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private float duration;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BreakSomething());
    }

    public IEnumerator BreakSomething()
    {
        float timer = 0f;
        Vector3 startPosition = transform.position;
        Vector3 startRotation = transform.rotation.eulerAngles;

        while (timer < duration)
        {
            // Interpolation linéaire entre les valeurs initiales et finales
            float t = timer / duration;
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, t));
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            timer += Time.deltaTime;
        }
    }
}
