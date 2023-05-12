using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;

    Vector3 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        hAxis = Input.GetAxisRaw("Horizontal");

        moveVec = new Vector3(0, 0, -hAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
    }
}
