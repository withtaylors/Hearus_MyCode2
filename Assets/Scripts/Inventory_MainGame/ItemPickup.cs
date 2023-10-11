using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    public int _itemID; // 아이템 아이디. 같은 아이템이라면 동일함.
    public int _count; // 습득 개수. 인벤토리에 몇 개씩 들어갈지를 정함.
    public int _fieldItemID; // 필드 아이템 아이디. 같은 아이템이라도 다른 오브젝트라면 다른 아이디를 가짐.

    public UnityEvent Picked;

    private ScriptManager scriptManager;
    private UnityEngine.Camera selectedCamera;
    private Vector3 objectPosition;
    private Quaternion objectRotation;
    private GameObject particlePrefab;
    private ParticleSystem particleSystem;

    private void Start()
    {
        scriptManager = FindObjectOfType<ScriptManager>(); // 스크립트 매니저: 아이템 스크립트를 재생할 때 쓰임
        selectedCamera = UnityEngine.Camera.main; // 메인 카메라

        objectPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z); // 해당 오브젝트의 위치 저장
        objectRotation = new Quaternion(-90, 0, 0, 90); // 파티클이 위로 나오도록 Rotation 설정

        particlePrefab = Resources.Load("Object_Particle") as GameObject; // Resources/Prefabs/Object Particle 로드
        GameObject instantiatedParticle = Instantiate(particlePrefab, objectPosition, objectRotation); // 인스턴스화
        instantiatedParticle.transform.SetParent(transform); // 파티클을 현재 오브젝트의 자식으로 지정함
        instantiatedParticle.transform.localScale = new Vector3(1, 1, 1);
        particleSystem = instantiatedParticle.GetComponent<ParticleSystem>(); // ParticleSystem 컴포넌트 가져오기
        if (!particleSystem.isPlaying)
            particleSystem.Play();
    }

    public void Pickup() // 아이템 줍기
    {
        Picked.Invoke(); // 아이템 습득 Invoke
        //CheckSwitch(_itemID);
        TextLogs.instance.GetItemLog(_itemID); // 아이템 습득 로그 생성
        Inventory.instance.GetAnItem(_itemID, _count); // 인벤토리에 넣기
        ChangeConditionMeet(_itemID); // isMeet이 false라면 true로 바꾸기

        Inventory.instance.SaveInventoryDataManager();
        InventoryDataManager.Instance.SaveFieldData(_fieldItemID, _itemID);
        DataManager.instance.SaveInventoryData();
    }

    private bool CheckObjectInCamera(GameObject item) // 오브젝트가 카메라 안에 있는지 확인
    {
        Vector3 itemPos = selectedCamera.WorldToViewportPoint(item.transform.position);

        if (itemPos.x >= 0 && itemPos.x <= 1 &&
            itemPos.y >= 0 && itemPos.y <= 1 && itemPos.z > 0)
            return true;
        else
            return false;
    }

    private void ChangeConditionMeet(int _itemID)
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (_itemID == ItemDatabase.itemList[i].itemID)
            {
                if (ItemDatabase.itemList[i].isMeet == false)
                    ItemDatabase.itemList[i].isMeet = true;
            }
        }
    }

    public void CheckSwitch(int _itemID)
    {
        if (_itemID == 122)
        {
            for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++)
            {
                if (DataManager.instance.dataWrapper.getItemIDList[i] == _itemID)
                {
                    if (ScriptSwitch.instance.switchs[0].switchValue == false) // 획득한 적이 있지만 약초인 것을 모를 때
                    {
                        scriptManager.FindScriptByEventName("MEET_AGAIN_122"); // MEET_AGAIN_124 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                    else                                                       // 획득한 적이 있고 약초인 것을 알 때
                    {
                        scriptManager.FindScriptByItemDesNum(_itemID);          // 아이템 설명 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                }
            }
        }

        else if (_itemID == 123)
        {
            for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++)
            {
                if (DataManager.instance.dataWrapper.getItemIDList[i] == _itemID)
                {
                    if (ScriptSwitch.instance.switchs[1].switchValue == false) // 획득한 적이 있지만 약초인 것을 모를 때
                    {
                        scriptManager.FindScriptByEventName("MEET_AGAIN_123"); // MEET_AGAIN_124 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                    else                                                       // 획득한 적이 있고 약초인 것을 알 때
                    {
                        scriptManager.FindScriptByItemDesNum(_itemID);          // 아이템 설명 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                }
            }
        }

        else if (_itemID == 124)
        {
            for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++)
            {
                if (DataManager.instance.dataWrapper.getItemIDList[i] == _itemID)
                {
                    if (ScriptSwitch.instance.switchs[2].switchValue == false) // 획득한 적이 있지만 약초인 것을 모를 때
                    {
                        scriptManager.FindScriptByEventName("MEET_AGAIN_124"); // MEET_AGAIN_124 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                    else                                                       // 획득한 적이 있고 약초인 것을 알 때
                    {
                        scriptManager.FindScriptByItemDesNum(_itemID);          // 아이템 설명 스크립트 재생
                        scriptManager.ShowScript();
                        return;
                    }
                }
            }
        }

        else if (_itemID == 107) // 은행
        {
            for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
            {
                if (Inventory.instance.inventoryItemList[i].itemID == 164) // 도끼가 있다면 break
                    break;
                else
                {
                    scriptManager.FindScriptByScriptID(141);
                    scriptManager.ShowScript();
                    return;
                }
            }
        }


        for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++)
        {
            if (DataManager.instance.dataWrapper.getItemIDList[i] == _itemID) // 획득한 적이 있다면
            {
                Debug.Log("획득한적잇음");
                scriptManager.FindScriptByItemDesNum(_itemID);          // 아이템 설명 스크립트 재생
                scriptManager.ShowScript();
                return;
            }
        }

        Debug.Log("획득한적없음");
        scriptManager.FindScriptByItemID(_itemID); // 해당 아이템 스크립트 찾기
        scriptManager.ShowScript(); // 스크립트 재생
    }
}