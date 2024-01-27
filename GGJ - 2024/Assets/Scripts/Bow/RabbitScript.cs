using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;     // Position de départ
    [SerializeField] private Vector3 endPosition;       // Position d'arrivée
    [SerializeField] private float oscillationSpeed; // Vitesse d'oscillation en unités par seconde

    void Update()
    {
        transform.position = startPosition + (endPosition - startPosition) * Mathf.PingPong(Time.time * oscillationSpeed, 1f); ;
    }
}
