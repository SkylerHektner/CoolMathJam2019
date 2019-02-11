using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float FollowOffsetZ = 10f;
    public float FollowOffsetY = 2f;
    public float LerpFactor = 5f;
    public Transform ObjectToTrack;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, FollowOffsetY, FollowOffsetZ);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ObjectToTrack.position + offset, Time.deltaTime * LerpFactor);
    }
}
