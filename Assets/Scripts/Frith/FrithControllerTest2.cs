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

    public float rotationSpeed = 90.0f; 

    void Start()
    {
        //rig = GetComponent<Rigidbody>();
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
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");

        // // 기본적으로 오른쪽(0도)을 바라보게 설정
        // float targetRotationY = 0;

        // // 왼쪽 방향
        // if (horizontalInput < 0)
        //     targetRotationY = 180;
        
        // // 위쪽 방향
        // else if (verticalInput > 0)
        //     targetRotationY = 90;

        // // 아래쪽 방향
        // else if (verticalInput < 0)
        //     targetRotationY = -90;

        // float currentAngleY = transform.eulerAngles.y;
        
        // float newAngleY = Mathf.MoveTowardsAngle(currentAngleY, targetRotationY, rotationSpeed * Time.deltaTime);
        
        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newAngleY, transform.rotation.eulerAngles.z);
        
        // 플레이어를 바라보도록 펫의 회전을 설정
        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y; // 펫과 플레이어의 높이가 같아야 함
        transform.LookAt(lookAtPosition);
    }
}