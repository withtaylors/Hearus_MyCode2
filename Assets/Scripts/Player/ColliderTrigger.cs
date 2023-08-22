using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public GameObject triggerObject1; // 트리거 오브젝트 1
    public GameObject triggerObject2; // 트리거 오브젝트 2
    private playerController playerController; // 플레이어 컨트롤러 스크립트 참조

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObject1 || other.gameObject == triggerObject2)
        {
            playerController.UpdateGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == triggerObject1 || other.gameObject == triggerObject2)
        {
            playerController.UpdateGrounded(false);
        }
    }
}