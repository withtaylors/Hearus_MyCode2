using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithControllerTest5 : MonoBehaviour
{
    public float speed;
    public float distance;

    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    public LayerMask groundLayer;
    public Transform player;

    public float distanceBehindPlayer = 2.0f; // 플레이어 뒤의 거리를 조절

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        FloatUpDown();

        // 플레이어 방향으로 펫을 직접 이동
        // Vector3 moveDirection = (player.position - transform.position).normalized;
        // moveDirection.y = 0;
        // transform.Translate(moveDirection * speed * Time.deltaTime);
        
        // Vector3 moveDirection = player.position - player.forward * distanceBehindPlayer;
        // moveDirection.y += 3;
        //transform.Translate(moveDirection * speed * Time.deltaTime);

        // 플레이어를 따라가도록 펫의 위치를 설정
        Vector3 targetPosition = player.position - player.forward * distanceBehindPlayer;
        targetPosition.y += 3;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 50);

        // 플레이어를 바라보도록 펫의 회전을 설정
        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y; // 펫과 플레이어의 높이가 같아야 함
        transform.LookAt(lookAtPosition);

        // 플레이어가 펫을 앞지르면 펫을 뒤로 이동
        if (Vector3.Distance(transform.position, player.position) < distanceBehindPlayer)
        {
            transform.position = player.position - player.forward * distanceBehindPlayer;
        }
    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }    
}