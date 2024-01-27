using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField, Range(1, 15)] private float followSpeed;
    private bool isFollowing = true;

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            Vector3 newPos = offset + follow.position;
            newPos.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
