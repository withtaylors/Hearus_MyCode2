using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 CameraOffset;

    void FixedUpdate()
    {
        transform.position = targetTransform.position + CameraOffset;
    }
}