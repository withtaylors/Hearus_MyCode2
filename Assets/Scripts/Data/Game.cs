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

    public GameObject edenGameObject;
    public GameObject noahGameObject;
    public GameObject adamGameObject;
    public GameObject jonahGameObject;

    void Start()
    {
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
                    playerTransform.position = new Vector3(DataManager.instance.nowPlayer.x, DataManager.instance.nowPlayer.y, DataManager.instance.nowPlayer.z);
                    //playerTransform.position = new Vector3(-40f, 10f, 0.5f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
                {
                    Debug.Log("start 태초의숲 -> 비탄의바다");
                    playerTransform.position = new Vector3(-16f, -125f, 145f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
                {
                    Debug.Log("start 비탄의바다 -> 타오르는황야");
                    playerTransform.position = new Vector3(-325f, -515f, 100f);
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
                    playerTransform.position = new Vector3(-1140f, -187.6f, 285f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
                {
                    Debug.Log("start 태초의숲 -> 비탄의바다");
                    playerTransform.position = new Vector3(-1128f, -149f, 216f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
                {
                    Debug.Log("start 비탄의바다 -> 타오르는황야");
                    playerTransform.position = new Vector3(-325f, -520f, 100f);
                }
            }


            if (DataManager.instance.nowPlayer.nowCharacter != "None")
            {
                switch (DataManager.instance.nowPlayer.nowCharacter)
                {
                    case "Eden":
                        edenGameObject.SetActive(true);
                        break;
                    case "Noah":
                        noahGameObject.SetActive(true);
                        break;
                    case "Adam":
                        adamGameObject.SetActive(true);
                        break;
                    case "Jonah":
                        jonahGameObject.SetActive(true);
                        break;
                }
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

    public void GameNext(bool state)
    {
        if (state == true)
        {
            DataManager.instance.nowPlayer.gameNext = state;
        }
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("종료해서 저장됨");
        //DataManager.instance.SaveData(DataManager.instance.nowSlot);
        Destroy(DataManager.instance.gameObject);
        DataManager.instance = null;
    }
}