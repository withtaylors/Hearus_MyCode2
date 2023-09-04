using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithController : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public float followSpeed = 3f; // 펫의 따라가는 속도
    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    private Vector3 initialOffset; // 초기 위치 오프셋
    private Quaternion initialRotationOffset; // 초기 회전 오프셋

    private void Start()
    {
        initialOffset = player.right * -1f + player.up + player.forward * 2f; // 플레이어의 왼쪽 상단에 위치하도록 수정
        transform.position = player.position + initialOffset;

        initialRotationOffset = Quaternion.Euler(0f, 90f, 0f); // 초기 회전 각도를 설정합니다.
    }

    private void Update()
    {
        FollowPlayer();
        FloatUpDown();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + initialOffset;

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;

        RotateToPlayerDirection(targetPosition - transform.position);
    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }

    private void RotateToPlayerDirection(Vector3 direction)
    {
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction) * initialRotationOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10f * Time.deltaTime);
        }
    }
}