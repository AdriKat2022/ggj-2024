using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurbrillanceCookie : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}
