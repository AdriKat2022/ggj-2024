using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiePlusUn : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField] private float speed;            // Vitesse de d�placement
    [SerializeField] private float jumpForce;        // Force du saut
    [SerializeField] private float jumpDuration;   // Dur�e du saut
    [SerializeField] private float destroyDuration;   // Dur�e du saut
    [SerializeField] private Rigidbody2D rb;   // Dur�e du saut

    [Header("ROtation")]
    [SerializeField] private float initialRotationMin;
    [SerializeField] private float initialRotationMax;
    [SerializeField] private float finalRotationMin;
    [SerializeField] private float finalRotationMax;
    private Quaternion targetRotation;

    void Start()
    {
        Destroy(gameObject, destroyDuration);
        rb.velocity = new Vector2((Random.Range(0, 2) * 2 - 1) * speed, jumpForce);
        targetRotation = Quaternion.Euler(0f, 0f, Random.Range(finalRotationMin, finalRotationMax));

    }

    private void Update()
    {
        // Rotation finale al�atoire
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }

}
