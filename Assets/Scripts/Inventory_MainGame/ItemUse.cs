using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
//using static UnityEditor.Progress;

// 아이템 사용을 구현한 스크립트. 아이템의 효과마다 다름.

public class ItemUse : MonoBehaviour
{
    public UnityEvent Used;

    public GameObject ropeForBridge;

    private Item currentItem;

    public void OnClickUseButton()
    {
        currentItem = Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot];

        if (Inventory.instance.inventoryItemList.Count > 0)
        {
            Used.Invoke();

            if (currentItem.itemEffect == Item.ItemEffect.회복) // 회복 아이템일 경우
                PlayerHP.instance.IncreaseHP(currentItem.effectValue);
            else if (currentItem.itemEffect == Item.ItemEffect.피해) // 피해 아이템일 경우
                PlayerHP.instance.DecreaseHP(currentItem.effectValue);
    
            if (currentItem.itemCount > 1)
                InventorySlot.instance.DecreaseCount(currentItem);
            else
                Inventory.instance.DeleteItem(currentItem.itemID);
        }
        else
            Debug.Log("인벤토리에 항목이 존재하지 않습니다.");
    }


}
