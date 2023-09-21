using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftRespawn : MonoBehaviour
{
    [SerializeField] private Transform Raft;
    [SerializeField] private Transform respawnPoint2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Raft.transform.position = respawnPoint2.transform.position;           
        }
    }
}