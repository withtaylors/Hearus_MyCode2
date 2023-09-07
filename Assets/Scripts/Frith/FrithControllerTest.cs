using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithControllerTest : MonoBehaviour
{
    public float speed;
    public float distance;
    public float jumpPower;

    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    public LayerMask groundLayer;

    public Transform player;
    public Rigidbody rig;

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
            transform.Translate(moveDirection * speed * Time.deltaTime);
            DirectionPet();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.5f, groundLayer)) // Raycast 수정
            {
                rig.velocity = Vector3.up * jumpPower;
            }
        }
    }

    void DirectionPet()
    {
        // 플레이어와 펫의 상대 위치 계산
        Vector3 relativePosition = player.position - transform.position;

        // 상대 위치의 각도 계산
        float angle = Mathf.Atan2(relativePosition.x, relativePosition.z) * Mathf.Rad2Deg;

        // 펫의 회전 각도 설정
        transform.eulerAngles = new Vector3(0, angle, 0);

        //transform.LookAt(player);

    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }
}