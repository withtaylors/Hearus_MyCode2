using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithControllerTest : MonoBehaviour
{
    public float speed;
    public float distance;
    public float jumpPower;

    public LayerMask groundLayer;

    Transform player;

    Rigidbody rig;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        Physics.IgnoreLayerCollision(9, 10);
    }

    void Update()
    {
        // 펫의 y 위치를 플레이어의 y 위치보다 항상 높게 유지
        Vector3 newPosition = transform.position;
        newPosition.y = Mathf.Max(newPosition.y, player.position.y);

        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            newPosition += moveDirection * speed * Time.deltaTime;
            DirectionPet();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.5f, groundLayer))
            {
                rig.velocity = Vector3.up * jumpPower;
            }
        }

        // 펫의 위치를 업데이트
        transform.position = newPosition;
    }

    void DirectionPet()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
