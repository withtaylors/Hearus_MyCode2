using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDebuff : MonoBehaviour
{
    public bool hasPassedTime_113; // 송이버섯 습득 후 시간 경과 여부.

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPickedItem(int _itemID) // 아이템 습득 시 호출되는 함수.
    {
        if (_itemID == 113)
        {
            Debug.Log("송이버섯을 습득했다.");
            StartCoroutine("ChangeItemEffectToDamage", 113); 
        }
    }

    public void CheckDebuffItem() // 디버프 아이템 사용 시 호출되는 함수.
    {

    }

    IEnumerable ChangeItemEffectToDamage(int _itemID) // 일정 시간 이후 아이템 이펙트를 "피해"로 바꾸는 코루틴.
    {
        Debug.Log("코루틴이 실행되었다.");
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (_itemID == ItemDatabase.itemList[i].itemID)
            {
                //yield return new WaitForSeconds(1200f);
                yield return new WaitForSeconds(3f);
                hasPassedTime_113 = true;
                ItemDatabase.itemList[i].itemEffect = Item.ItemEffect.피해;
                Debug.Log("송이버섯이 먹을 수 없는 음식으로 바뀌었다.");
            }
        }
    }
}
