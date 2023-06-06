using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

// 아이템 사용을 구현한 스크립트. 아이템의 효과마다 다름.

public class ItemUse : MonoBehaviour
{
    public void OnClickUseButton()
    {
        if (Inventory.instance.inventoryItemList.Count > 0)
        {
            if (Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemEffect == Item.ItemEffect.회복) // 회복 아이템일 경우
            {
                PlayerHP.instance.IncreaseHP(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemEffectValue);
            }
            else if (Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemEffect == Item.ItemEffect.피해) // 피해 아이템일 경우
            {
                PlayerHP.instance.DecreaseHP(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemEffectValue);
            }
    
            if (Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemCount > 1)
            {
                InventorySlot.instance.DecreaseCount(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]);
            }
            else
            {
                Inventory.instance.DeleteItem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]);
            }
        }
        else
        {
            Debug.Log("인벤토리에 항목이 존재하지 않습니다.");
        }

    }
}
