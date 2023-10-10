using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftRespawn : MonoBehaviour
{
    public GameObject raft;
    [SerializeField] private Transform respawnPoint2;

    // CrossWater 스크립트의 인스턴스
    private CrossWater crossWaterScript;
    public bool isPlayerOnWater;
    private ObjectAppearOnCollision objectAppearOnCollisionScript;
    public bool isFadingIn;

    private void Start()
    {
        crossWaterScript = FindObjectOfType<CrossWater>();
        objectAppearOnCollisionScript = FindObjectOfType<ObjectAppearOnCollision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crossWaterScript.isPlayerOnWater = false");

        if (other.CompareTag("Player"))
        {
            // crossWaterScript null 체크 추가
            if (crossWaterScript != null)
            {
                // 물체 위치를 재설정
                raft.transform.position = respawnPoint2.transform.position;
                raft.SetActive(false);

                crossWaterScript.isPlayerOnWater = false;
                objectAppearOnCollisionScript.isFadingIn = false;
                Debug.Log("crossWaterScript.isPlayerOnWater = " + crossWaterScript.isPlayerOnWater);
            }
        }
    }
}
