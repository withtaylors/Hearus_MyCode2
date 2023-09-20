using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform playerTransform;

    public string filename;

    public bool gameNext;
    public bool gameBefore;
    public string currentMap;

    //플레이어 위치
    public float x;
    public float y;
    public float z;

    public string nowCharacter;

    void Start()
    {
        // 플레이어 위치 설정
        // gameNext 상태 초기설정
        // GameNext(DataManager.instance.nowPlayer.gameNext);

        // gameNext = true (맵끝까지 갔을때)
        if(DataManager.instance.nowPlayer != null)
        {
            Debug.Log("start 최초 if");

            if (DataManager.instance.nowPlayer.gameNext == true)
            {
                Debug.Log("start - gameNext = true 인식");

                if (DataManager.instance.nowPlayer.currentMap == "태초의숲")
                {
                    Debug.Log("start 태초의숲");
                    playerTransform.position = new Vector3(-40f, 10f, 0.5f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "태초의숲 -> 비탄의바다")
                {
                    Debug.Log("start 태초의숲 -> 비탄의바다");
                    playerTransform.position = new Vector3(-16f, -125f, 145f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다 -> 사막")
                {
                    Debug.Log("start 비탄의바다 -> 사막");
                    playerTransform.position = new Vector3(-0f, -0f, -0f);
                }
            }
            // gameNext = false (맵 진입 후 게임중)
            else 
            {
                Debug.Log("start - gameNext = false 인식");
                playerTransform.position = new Vector3(DataManager.instance.nowPlayer.x, DataManager.instance.nowPlayer.y, DataManager.instance.nowPlayer.z);
            }

            if (DataManager.instance.nowPlayer.gameBefore == true)
            {
                Debug.Log("start - gameBefore = true 인식");

                if (DataManager.instance.nowPlayer.currentMap == "태초의숲")
                {
                    Debug.Log("start 태초의숲");
                    playerTransform.position = new Vector3(-1140f, -183f, 285f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "태초의숲 -> 비탄의바다")
                {
                    Debug.Log("start 태초의숲 -> 비탄의바다");
                    playerTransform.position = new Vector3(-16f, -125f, 145f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다 -> 사막")
                {
                    Debug.Log("start 비탄의바다 -> 사막");
                    playerTransform.position = new Vector3(-0f, -0f, -0f);
                }
            }
            else 
            {
                Debug.Log("start 최초 else");
                playerTransform.position = new Vector3(-40f, 10f, 0.5f);
            }
        }
    }

    void Update()
    {
        // 플레이어 위치 update
        DataManager.instance.nowPlayer.x = playerTransform.position.x;
        DataManager.instance.nowPlayer.y = playerTransform.position.y;
        DataManager.instance.nowPlayer.z = playerTransform.position.z;        
    }

    public void Save()
    {
    	DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void GameNext(bool state)
    {
        if (state == true)
        {
            DataManager.instance.nowPlayer.gameNext = state;
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("종료해서 저장됨");
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }
}