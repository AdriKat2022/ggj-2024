using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField, Range(1, 15)] private float followSpeed;

    [SerializeField] private Transform minBoundTr;
    [SerializeField] private Transform maxBoundTr;

    private Vector3 minBound;
    private Vector3 maxBound;

    private bool isFollowing = true;

    private void Start()
    {
        isFollowing = true;

        if (minBoundTr)
        {
            minBound = minBoundTr.position;
        }
        else
        {
            minBound = Vector3.negativeInfinity;
        }

        if (maxBoundTr)
        {
            maxBound = maxBoundTr.position;
        }
        else
        {
            maxBound = Vector3.positiveInfinity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            Vector3 newPos = offset + follow.position;
            newPos = new Vector3(Mathf.Clamp(newPos.x, minBound.x, maxBound.x), Mathf.Clamp(newPos.y, minBound.y, maxBound.y), transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
