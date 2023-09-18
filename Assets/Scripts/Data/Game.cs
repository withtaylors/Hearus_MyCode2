using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform playerTransform;

    public string filename;

    public bool gameEnd;
    public string currentMap;

    //플레이어 위치
    public float x;
    public float y;
    public float z;

    public string nowCharacter;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 위치 설정
        if (DataManager.instance.nowPlayer == null)
        {
            Debug.Log("game start first start 호출됨");
            playerTransform.position = new Vector3(40.6f, 8.5f, 0.5f);        
        }
        else
        {
            Debug.Log("game start not the first 호출됨");
            playerTransform.position = new Vector3(DataManager.instance.nowPlayer.x, DataManager.instance.nowPlayer.y, DataManager.instance.nowPlayer.z);
        }
        GameEnd(DataManager.instance.nowPlayer.gameEnd);

        if (DataManager.instance.nowPlayer.gameEnd == true)
        {
            Debug.Log("game start - gameend true 호출됨");

            if (DataManager.instance.nowPlayer.currentMap == "태초의숲 -> 비탄의바다")
            {
                Debug.Log("start 태초의숲 -> 비탄의바다 호출됨");
                playerTransform.position = new Vector3(-16f, -125f, 145f);
            }
            else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다 -> 사막")
            {
                Debug.Log("start 비탄의바다 -> 사막");
                playerTransform.position = new Vector3(-0f, -0f, -0f);
            }
            // DataManager.instance.nowPlayer.gameEnd = false;
            // Debug.Log("start gameEnd = false로 변경함");
        }
        else //gameEnd = false일때 즉, 새로운맵에 진입하고 난 후 움직임
        {
            Debug.Log("game start - gameend =false 로 인식");
            // 플레이어 위치 update
            DataManager.instance.nowPlayer.x = playerTransform.position.x;
            DataManager.instance.nowPlayer.y = playerTransform.position.y;
            DataManager.instance.nowPlayer.z = playerTransform.position.z;
        }
    }

    void Update()
    {
        Debug.Log("game update 호출됨");

        if (DataManager.instance.nowPlayer.currentMap == "태초의숲 -> 비탄의바다")
        {
            Debug.Log("game update - currentmap 호출됨");

            if (playerTransform.position.x != -16f && playerTransform.position.z != 145f)
            {
                Debug.Log("game update xyz 위치다름 호출됨 - gameEnd =false로");

                DataManager.instance.nowPlayer.gameEnd = false;
            }
        }

        // //플레이어가 새로운 맵에 진입했을 시
        // if (DataManager.instance.nowPlayer.gameEnd == true)
        // {
        //     Debug.Log("game update - gameend true 호출됨");

        //     if (DataManager.instance.nowPlayer.currentMap == "태초의숲 -> 비탄의바다")
        //     {
        //         Debug.Log("태초의숲 -> 비탄의바다 호출됨");
        //         playerTransform.position = new Vector3(-16f, -125f, 145f);
        //     }
        //     else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다 -> 사막")
        //     {
        //         Debug.Log("비탄의바다 -> 사막");
        //         playerTransform.position = new Vector3(-0f, -0f, -0f);
        //     }
        //     // DataManager.instance.nowPlayer.gameEnd = false;
        //     // Debug.Log("gameEnd = false로 변경함");
        // }
        // else //gameEnd = false일때 즉, 새로운맵에 진입하고 난 후 움직임
        // {
        //     Debug.Log("game update - gameend =false 로 인식");
        //     // 플레이어 위치 update
        //     DataManager.instance.nowPlayer.x = playerTransform.position.x;
        //     DataManager.instance.nowPlayer.y = playerTransform.position.y;
        //     DataManager.instance.nowPlayer.z = playerTransform.position.z;        
        // }
    }

    public void Save()
    {
    	DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void GameEnd(bool state)
    {
        if (state.Equals("true"))
        {
            DataManager.instance.nowPlayer.gameEnd = state;
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.SaveData(DataManager.instance.nowSlot); // 파라미터 추가
    }
}