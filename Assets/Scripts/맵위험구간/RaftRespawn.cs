using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftRespawn : MonoBehaviour
{
    public GameObject raft;
    [SerializeField] private Transform respawnPoint2;
    private ObjectAppearOnCollision objectAppearOnCollisionScript;
    public bool isFadingIn;

    private void Start()
    {
        objectAppearOnCollisionScript = FindObjectOfType<ObjectAppearOnCollision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("crossWaterScript.isPlayerOnWater");

        if (other.CompareTag("Player"))
        {
            // 물체 위치를 재설정
            raft.transform.position = respawnPoint2.transform.position;
            raft.SetActive(false);

            CrossWater.isPlayerOnWater = false;
            objectAppearOnCollisionScript.isFadingIn = false;
        }
    }
}
