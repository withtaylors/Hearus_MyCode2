using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Crafting : MonoBehaviour
{
    private List<InventorySlot> craftingSlots;
    public Transform tf_craftingSlots;

    public List<Item> craftingItemList;

    public CraftingCombination craftingCombination; // 크래프팅 조합 객체

    private void Start()
    {
        craftingSlots = new List<InventorySlot>(tf_craftingSlots.GetComponentsInChildren<InventorySlot>());
        craftingItemList = new List<Item>();
    }

    public void onCiickSelectButton()
    {
        if (craftingItemList.Count > 0)
        {
            for (int i = 0; i < craftingItemList.Count; i++) // 크래프팅 아이템 리스트에 동일한 아이템이 있는지 검사
            {
                if (craftingItemList[i].itemID == Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemID)
                {
                    CheckItemAmount(i);
                    craftingItemList[i].itemCount += 1;
                    craftingSlots[i].UpdateItemCount(craftingItemList[i]);
                    return;
                }
            }

        if (craftingItemList.Count == 3)                     // 슬롯이 전부 다 찬 경우
            {
                Debug.Log("빈 슬롯이 없습니다.");
                return;
            }
        }

        SelectItem();

        return;
    }

    public void onClickCraftingButton()
    {
        if (craftingItemList.Count < 1)
        {
            Debug.Log("아이템을 하나 이상 담으세요.");
            return;
        }
        else
        {
            Inventory.instance.GetAnItem(CheckCombination());
            DestroyItem();
            ResetCraftingSlot();
            ResetCraftingList();
        }
    }

    public void InsertSlot(Item _item) // 크래프팅 슬롯에 추가
    {
        if (craftingItemList.Count > 0)
        {
            craftingSlots[craftingItemList.Count - 1].Additem(_item);
        }
        return;
    }

    public void CheckItemAmount(int i) // 크래프팅 슬롯 내 수량 제한
    {
        if (craftingItemList[i].itemCount >= Inventory.instance.inventoryItemList[i].itemCount)
        {
            Debug.Log("보유 수량 이상을 담을 수 없습니다.");
            return;
        }
    }

    public int CheckCombination() // 아이템 조합 유효 검사
    {
        if (craftingItemList.Count == 1)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
                if ((craftingItemList[0].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].firstCount))
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    return craftingCombination.combinations[i].outputID;
                }
            }
        }
        else if (craftingItemList.Count == 2)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
                if (((craftingItemList[0].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].firstCount)) ||
                    ((craftingItemList[0].itemID == craftingCombination.combinations[i].secondID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].secondCount)) ||

                    ((craftingItemList[1].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[1].itemCount == craftingCombination.combinations[i].firstCount)) ||
                    ((craftingItemList[1].itemID == craftingCombination.combinations[i].secondID) &&
                    (craftingItemList[1].itemCount == craftingCombination.combinations[i].secondCount))) 
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    return craftingCombination.combinations[i].outputID;
                }
            }
        }
        else if (craftingItemList.Count == 3)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
                if ((((craftingItemList[0].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].firstCount)) ||
                    ((craftingItemList[0].itemID == craftingCombination.combinations[i].secondID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].secondCount)) ||
                    ((craftingItemList[0].itemID == craftingCombination.combinations[i].thirdID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].thirdCount))) &&

                    (((craftingItemList[1].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[1].itemCount == craftingCombination.combinations[i].firstCount)) ||
                    ((craftingItemList[1].itemID == craftingCombination.combinations[i].secondID) &&
                    (craftingItemList[1].itemCount == craftingCombination.combinations[i].secondCount)) ||
                    ((craftingItemList[1].itemID == craftingCombination.combinations[i].thirdID) &&
                    (craftingItemList[1].itemCount == craftingCombination.combinations[i].thirdCount))) &&

                    (((craftingItemList[2].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[2].itemCount == craftingCombination.combinations[i].firstCount)) ||
                    ((craftingItemList[2].itemID == craftingCombination.combinations[i].secondID) &&
                    (craftingItemList[2].itemCount == craftingCombination.combinations[i].secondCount)) ||
                    ((craftingItemList[2].itemID == craftingCombination.combinations[i].thirdID) &&
                    (craftingItemList[2].itemCount == craftingCombination.combinations[i].thirdCount))))
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    return craftingCombination.combinations[i].outputID;
                }
            }
        }
            Debug.Log("유효하지 않은 조합입니다.");
            return 0;
    }

    public void SelectItem() // 아이템을 선택해 조합 슬롯으로 복사
    {
        Item SelectedInventoryItem = Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot];
        Item ClonedItem = CloneItem(SelectedInventoryItem);
        craftingItemList.Add(ClonedItem);
        InsertSlot(ClonedItem);
    }

    public void DestroyItem() // 재료를 인벤토리에서 삭제
    {
        if (craftingItemList.Count == 1)
        {
            for (int i = 0; i < craftingItemList[0].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[0].itemID);
        }
        else if (craftingItemList.Count == 2)
        {
            for (int i = 0; i < craftingItemList[0].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[0].itemID);
            for (int i = 0; i < craftingItemList[1].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[1].itemID);
        }
        else if (craftingItemList.Count == 3)
        {
            for (int i = 0; i < craftingItemList[0].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[0].itemID);
            for (int i = 0; i < craftingItemList[1].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[1].itemID);
            for (int i = 0; i < craftingItemList[2].itemCount; i++)
                Inventory.instance.DeleteItem(craftingItemList[2].itemID);
        }
    }

    public void ResetCraftingList() // 크래프팅 아이템 리스트를 초기화
    {
        craftingItemList.Clear();
    }

    public void ResetCraftingSlot()
    {
        for (int i = 0; i < craftingItemList.Count; i++)
        {
            craftingSlots[i].RemoveItem();
        }
    }

    public Item CloneItem(Item _item) // 아이템 복사
    {
        Item clonedItem = new Item(_item.itemID, _item.itemName, _item.itemDescription, _item.itemType, _item.itemEffect, _item.effectValue, 1);
        return clonedItem;
    }
}
