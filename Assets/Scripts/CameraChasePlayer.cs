using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChasePlayer : MonoBehaviour
{
    public Transform target;

    public float speed;

    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 final_target = target.position + offset;

        //transform.position = new Vector3(final_target.x, final_target.y, final_target.z);
        transform.position = Vector3.Lerp(transform.position, final_target, Time.deltaTime * speed);
    }
}
