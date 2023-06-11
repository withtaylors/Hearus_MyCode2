using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using TMPro;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    private List<InventorySlot> craftingSlots; // 크래프팅 슬롯 리스트
    public Transform tf_craftingSlots; // 크래프팅 슬롯 관리를 위한 부모

    public List<Item> craftingItemList; // 크래프팅 아이템 리스트

    public CraftingCombination craftingCombination;
    public GameObject craftingLogPanel;
    public TextMeshProUGUI craftingLogText;

    private void Start()
    {
        craftingSlots = new List<InventorySlot>(tf_craftingSlots.GetComponentsInChildren<InventorySlot>());
        craftingItemList = new List<Item>();
    }

    private void FixedUpdate()
    {
        if (craftingItemList.Count > 0)
        {
            if (Inventory.instance.activated == false) // 인벤토리가 비활성화되면 크래프팅 슬롯 및 리스트가 초기화, 로그도 비활성화
            {
                ResetCraftingSlot();
                ResetCraftingList();
                craftingLogPanel.SetActive(false);
                craftingLogText.gameObject.SetActive(false);
            }
        }
    }

    public void onCiickSelectButton() // 선택 버튼을 눌렀을 때
    {
        if (craftingItemList.Count > 0)
        {
            if (craftingItemList.Count == 3)                 // 크래프팅 아이템 리스트가 다 찼는지 검사
            {
                GetCraftingLog("! 빈 슬롯이 없습니다. !");
                return;
            }

            for (int i = 0; i < craftingItemList.Count; i++) // 크래프팅 아이템 리스트에 동일한 아이템이 있는지 검사
            {
                if (craftingItemList[i].itemID == Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemID) // 동일한 아이템이 있다면
                {
                    if (CheckItemAmount(i) == true)                                                                             // 수량 먼저 검사한 후 수량 추가
                    {
                        craftingItemList[i].itemCount += 1;
                        craftingSlots[i].UpdateItemCount(craftingItemList[i]);
                        return;
                    }
                    else
                    {
                        GetCraftingLog("! 보유 수량 이상을 담을 수 없습니다. !");
                        return;
                    }
                }
            }
            SelectItem(); // 동일한 아이템이 없다면 슬롯에 추가
            return;
        }
        else if (craftingItemList.Count == 0) // 아이템 리스트가 비어 있으면 그냥 추가
        {
            SelectItem();
            return;
        }
    }

    public void onClickCraftingButton()
    {
        int returnID = CheckCombination();

        if (craftingItemList.Count < 1)
        {
            GetCraftingLog("! 아이템을 하나 이상 선택하세요. !");
            return;
        }
        else
        {
            if (returnID == 0)
            {
                GetCraftingLog("! 유효하지 않은 조합입니다. !");
                return;
            }
            else
            {
                Inventory.instance.GetAnItem(returnID);
                DestroyItem();
                ResetCraftingSlot();
                ResetCraftingList();
                return;
            }

        }
    }

    public void InsertSlot(Item _item) // 크래프팅 슬롯에 추가
    {
        if (craftingItemList.Count > 0)
        {
            craftingSlots[craftingItemList.Count - 1].Additem(_item);
        }
        else 
            GetCraftingLog("! 조합 리스트가 비어 있습니다. !");
        return;
    }

    public bool CheckItemAmount(int i) // 크래프팅 슬롯 내 수량 제한
    {
        if (craftingItemList[i].itemCount >= Inventory.instance.inventoryItemList[i].itemCount)
            return false;
        else 
            return true;
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
        GetCraftingLog("! 유효하지 않은 조합입니다. !");
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
        Item clonedItem = new Item(_item.itemID, _item.itemName, _item.itemDescription, _item.itemType, _item.itemEffect, _item.effectValue, 1, _item.isMeet, _item.isPicking);
        return clonedItem;
    }

    private void GetCraftingLog(string _log)
    {
        craftingLogPanel.SetActive(true);
        craftingLogText.gameObject.SetActive(true);

        craftingLogText.text = _log;
    }
}
