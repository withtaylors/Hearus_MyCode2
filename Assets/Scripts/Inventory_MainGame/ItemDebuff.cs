using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDebuff : MonoBehaviour
{
    public static ItemDebuff instance;

    public bool hasPassedTime_113; // 송이버섯 습득 후 시간 경과 여부.
    public UnityEvent HasPassedTime_113;

    private void Awake()
    {
        // Singleton
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CheckPickedItem(int _itemID) // 아이템 습득 시 호출되는 함수.
    {
        if (_itemID == 113)
        {
            Debug.Log("송이버섯을 습득했다.");
            StartCoroutine(ChangeItemEffectToDamage()); 
        }
    }

    public void CheckDebuffItem() // 디버프 아이템 사용 시 호출되는 함수.
    {

    }

    IEnumerator ChangeItemEffectToDamage() // 일정 시간 이후 아이템 이펙트를 "피해"로 바꾸는 코루틴.
    {
        for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
        {
            if (Inventory.instance.inventoryItemList[i].itemID == 113) // 제일 먼저 발견되는 송이버섯
            {
                yield return new WaitForSeconds(1200f); // 20분 뒤 먹을 수 먹을 수 없는 음식으로 바뀜.
                hasPassedTime_113 = true;
                Inventory.instance.inventoryItemList[i].itemEffect = Item.ItemEffect.피해;
                Debug.Log("송이버섯이 먹을 수 없는 음식으로 바뀌었다.");
            }
        }
    }
}
