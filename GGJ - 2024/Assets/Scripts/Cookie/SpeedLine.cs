using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLine : MonoBehaviour
{
    [SerializeField] private float breathingSpeed;   // Vitesse de respiration
    [SerializeField] private float breathingAmount; // Amplitude de la respiration

    [SerializeField] private float minScale;        // �chelle minimale
    [SerializeField] private float maxScale;          // �chelle maximale

    void Update()
    {
        float breathingScale = minScale + Mathf.Sin(Time.time * breathingSpeed) * maxScale;
       
        transform.localScale = new Vector3(breathingScale, breathingScale, breathingScale);
    }
}
