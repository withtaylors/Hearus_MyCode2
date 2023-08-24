using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisController : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public float followSpeed = 5f; // 펫의 따라가는 속도
    public float verticalSpeed = 1f; // 위아래로 움직이는 속도
    public float verticalRange = 0.5f; // 위아래 움직임의 범위

    private Vector3 initialOffset; // 초기 위치 오프셋

    private void Start()
    {
        initialOffset = transform.position - player.position;
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
    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }
}
