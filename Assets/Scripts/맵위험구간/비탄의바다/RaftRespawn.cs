using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftRespawn : MonoBehaviour
{
    public GameObject raft;
    [SerializeField] private Transform respawnPoint2;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 물체 위치를 재설정
            raft.transform.position = respawnPoint2.transform.position;
            raft.SetActive(true);

            CrossWater.isPlayerOnWater = false;
        }
    }
}
