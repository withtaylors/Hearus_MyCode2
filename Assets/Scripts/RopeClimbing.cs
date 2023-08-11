using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeClimbing : MonoBehaviour
{
    public Transform character; // 등반자 캐릭터 Transform
    public Transform rope; // 로프 GameObject
    public float climbingSpeed = 5f;

    private bool isClimbing = false;

    void Update()
    {
        // 로프를 향해 캐릭터를 이동시킵니다.
        if (isClimbing)
        {
            Vector3 targetPosition = rope.position;
            targetPosition.y = character.position.y;
            character.position = Vector3.MoveTowards(character.position, targetPosition, climbingSpeed * Time.deltaTime);
        }

        // 등반 행동을 시작 또는 멈춥니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClimbing = !isClimbing;
        }
    }
}
