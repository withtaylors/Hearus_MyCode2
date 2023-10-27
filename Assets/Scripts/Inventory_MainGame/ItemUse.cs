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
    public GameObject player;

    private Item currentItem;
    private int currentItemID;


    public void OnClickUseButton()
    {
        currentItem = Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot];
        currentItemID = currentItem.itemID;

        if (Inventory.instance.inventoryItemList.Count > 0)
        {
            if (currentItemID == 102 && player.GetComponent<playerController>().canUseRope == true)
                ropeForBridge.SetActive(true);

            Inventory.instance.DeleteItem(currentItemID);

            if (currentItem.itemEffect == Item.ItemEffect.회복) // 회복 아이템일 경우
                PlayerHP.instance.IncreaseHP(currentItem.effectValue);
            else if (currentItem.itemEffect == Item.ItemEffect.피해) // 피해 아이템일 경우
                PlayerHP.instance.DecreaseHP(currentItem.effectValue);
            else
                Debug.Log("인벤토리에 항목이 존재하지 않습니다.");
        }
    }
}
