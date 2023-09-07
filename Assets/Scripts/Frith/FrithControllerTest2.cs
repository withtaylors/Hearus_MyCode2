using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithControllerTest2 : MonoBehaviour
{
    public float speed;
    public float distance;

    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    public LayerMask groundLayer;

    public Transform player;
    public Rigidbody rig;

    public float rotationSpeed = 5.0f; 

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        FloatUpDown();

        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            moveDirection.y = 0;
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            DirectionPet();
        }
    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }

    void DirectionPet()
    {

    }
}
