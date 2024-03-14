using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCookie : MonoBehaviour
{
    [SerializeField] private float fallDuration;     // Durée de la chute
    [SerializeField] private float fallSpeed;    // Vitesse de rotation pendant le roulement
    [SerializeField] private float rotationFallSpeed;    // Vitesse de rotation pendant le roulement
    [SerializeField] private float rollDuration;       // Durée du roulement après la chute
    [SerializeField] private float rollSpeed;    // Vitesse de rotation pendant le roulement
    [SerializeField] private float rotationSpeed;    // Vitesse de rotation pendant le roulement

    private Transform parent;
    private PlayerMovement blo;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
        blo = FindObjectOfType<PlayerMovement>();
    }

    public IEnumerator BreakEverything()
    {
        
        // Chute
        float fallTimer = 0f;
        while (fallTimer < fallDuration)
        {
            // Ajuste la position pour simuler la chute
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
            transform.Rotate(rotationFallSpeed * Time.deltaTime * Vector3.forward, Space.World);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            fallTimer += Time.deltaTime;
        }

        // Roulement au sol
        float rollTimer = 0f;
        while (rollTimer < rollDuration)
        {
            // Ajuste la position pour simuler le roulement
            transform.Translate(Vector3.right * Time.deltaTime * rollSpeed, Space.World);

            // Ajuste la rotation pour simuler le roulement
            transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);

            // Attend la prochaine frame
            yield return null;

            // Met à jour le temps écoulé
            rollTimer += Time.deltaTime;
        }
        if(blo != null)
        {
            blo.canMove = true;
        }
        Destroy(parent.gameObject);
    }
}
