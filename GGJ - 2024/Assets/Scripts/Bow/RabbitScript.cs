using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScript : MonoBehaviour
{
    [SerializeField] private Animator anim;     // Position de d�part
    [SerializeField] private Vector3 startPosition;     // Position de d�part
    [SerializeField] private Vector3 endPosition;       // Position d'arriv�e
    [SerializeField] private float oscillationSpeed; // Vitesse d'oscillation en unit�s par seconde
    [SerializeField] private bool isDead; // Vitesse d'oscillation en unit�s par seconde
    [SerializeField] private bool willFlee; // Vitesse d'oscillation en unit�s par seconde


    void Update()
    {
        if (isDead)
        {
        }
        else
        {
            transform.position = startPosition + (endPosition - startPosition) * Mathf.PingPong(Time.time * oscillationSpeed, 1f); ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Munition _)) {
            anim.SetBool("Dead", true);
            isDead = true;
        }
    }
}
