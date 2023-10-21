using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLocation : MonoBehaviour
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
                    //playerTransform.position = new Vector3(29f, 10f, 0.5f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
                {
                    Debug.Log("start 비탄의바다");
                    playerTransform.position = new Vector3(-20f, -125f, 145f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
                {
                    Debug.Log("start 타오르는황야");
                    playerTransform.position = new Vector3(-325f, -515f, 100f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "파멸된도시")
                {
                    Debug.Log("start 파멸된도시");
                    playerTransform.position = new Vector3(-250f, -520f, 110f);
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
                    playerTransform.position = new Vector3(-1140f, -187f, 285f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
                {
                    Debug.Log("start 비탄의바다");
                    playerTransform.position = new Vector3(-1128f, -149f, 216f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
                {
                    Debug.Log("start 타오르는황야");
                    playerTransform.position = new Vector3(-325f, -520f, 100f);
                }
                else if (DataManager.instance.nowPlayer.currentMap == "파멸된도시")
                {
                    Debug.Log("start 파멸된도시");
                    playerTransform.position = new Vector3(-1265f, -520f, 210f);
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
        Debug.Log("종료해서 저장됨");
        DataManager.instance.SaveInventoryData();
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
        Destroy(DataManager.instance.gameObject);
        DataManager.instance.nowPlayer = null;
        DataManager.instance.DataClear();
        DataManager.instance.InventoryClear();
        //DataManager.instance.FieldDataClear();
    }
}