using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWater : MonoBehaviour
{
    public bool isPlayerOnWater = false;
    private Vector3 waterDirection;
    public float moveSpeed = 7f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnWater = true;
            // 저장된 방향을 물체의 방향으로 설정
            waterDirection = transform.forward.normalized;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnWater = false;
        }
    }

    void FixedUpdate()
    {
        if (isPlayerOnWater)
        {
            // 플레이어의 Rigidbody 컴포넌트를 얻어서 이동
            Rigidbody playerRigidbody = GetComponent<Rigidbody>();

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 플레이어가 뗏목 위에 있을 때만 움직이도록 수정
            if (Mathf.Approximately(horizontalInput, 0) && Mathf.Approximately(verticalInput, 0))
            {
                playerRigidbody.velocity = Vector3.zero;
            }
            else
            {
                // 입력을 반대로 적용
                Vector3 moveDirection = (-Vector3.forward * verticalInput + -Vector3.right * horizontalInput).normalized;
                Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + moveAmount);
            }
        }
    }
}
