using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;

public class PlayerLocation : MonoBehaviour
{
/*    public Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // 플레이어 이전 위치로, 새로운 시작이라면 기본 위치로
        if (DataManager.instance.nowPlayer != null)
        {
            playerTransform.position = new Vector3(DataManager.instance.nowPlayer.x, DataManager.instance.nowPlayer.y, DataManager.instance.nowPlayer.z);
        }
        else
        {
            playerTransform.position = new Vector3(DataManager.instance.nowPlayerDefault.x, DataManager.instance.nowPlayerDefault.y, DataManager.instance.nowPlayerDefault.z);
        }
    }

    public void Update()
    {
        // 플레이어 위치 update
        DataManager.instance.nowPlayer.x = playerTransform.position.x;
        DataManager.instance.nowPlayer.y = playerTransform.position.y;
        DataManager.instance.nowPlayer.z = playerTransform.position.z;
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.SaveData();
    }*/
}