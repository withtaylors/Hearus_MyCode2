using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

<<<<<<< HEAD
// 크래프팅 기능 구현 코드.

public class Crafting : MonoBehaviour
{
    public static Crafting instance;

=======
public class Crafting : MonoBehaviour
{
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    private List<InventorySlot> craftingSlots;
    public Transform tf_craftingSlots;

    public List<Item> craftingItemList;

    public CraftingCombination craftingCombination; // 크래프팅 조합 객체

<<<<<<< HEAD
    public GameObject go_Guide;
    public GameObject go_Inventory;
    public GameObject go_Page1;
    public GameObject go_TapPanel;

    public GameObject go_ExitButton;
    public GameObject go_BackButton;

    public bool isGuideActivated;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isGuideActivated = false;
        go_BackButton.SetActive(false);
=======
    private void Start()
    {
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
        craftingSlots = new List<InventorySlot>(tf_craftingSlots.GetComponentsInChildren<InventorySlot>());
        craftingItemList = new List<Item>();
    }

<<<<<<< HEAD
    private void Update()
    {
       if (isGuideActivated)
        {
            go_Guide.SetActive(isGuideActivated);
            go_BackButton.SetActive(isGuideActivated);
            go_Inventory.SetActive(!isGuideActivated);
            go_ExitButton.SetActive(!isGuideActivated);
        }
       else
        {
            go_Guide.SetActive(isGuideActivated);
            go_BackButton.SetActive(isGuideActivated);
            go_ExitButton.SetActive(!isGuideActivated);
        }

       if (!go_Page1.activeSelf || !go_TapPanel.activeSelf || go_Inventory.activeSelf)
        {
            isGuideActivated = false;
        }
    }

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    public void onCiickSelectButton()
    {
        if (craftingItemList.Count > 0)
        {
            for (int i = 0; i < craftingItemList.Count; i++) // 크래프팅 아이템 리스트에 동일한 아이템이 있는지 검사
            {
<<<<<<< HEAD
                if (craftingItemList[i].itemCount == Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemCount) // i번째
                {
                    Debug.Log("보유 수량 이상을 담을 수 없습니다.");
                    return;
                }

                if (craftingItemList[i].itemID == Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemID)
                {
                    craftingItemList[i].CloneItem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]);
                    craftingSlots[i].IncreaseCount(craftingItemList[i]);
=======
                CheckItemAmount(i);

                if (craftingItemList[i].itemID == Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].itemID)
                {
                    craftingItemList[i].itemCount += 1;
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
                    return;
                }
            }
        if (craftingItemList.Count == 3)                     // 슬롯이 전부 다 찬 경우
            {
                Debug.Log("빈 슬롯이 없습니다.");
                return;
            }
        }

<<<<<<< HEAD
        craftingItemList.Add(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].CloneItem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]));
=======
        craftingItemList.Add(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]);
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51

        InsertSlot();

        return;
    }

    public void onClickCraftingButton()
    {
<<<<<<< HEAD
        int output;

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
        if (craftingItemList.Count < 1)
        {
            Debug.Log("아이템을 하나 이상 담으세요.");
            return;
        }
        else
        {
<<<<<<< HEAD
            output = CheckCombination();

            if (output != 0)
            {
            Inventory.instance.GetAnItem(output); // 조합 결과물 인벤토리에 습득
            
            for (int i = 0; i < craftingItemList.Count; i++) // 크래프팅 슬롯 초기화
            {
                 craftingSlots[i].RemoveItem();
                 for (int j = 0; j < Inventory.instance.inventoryItemList.Count; j++)
                 {
                     if (craftingItemList[i].itemID == Inventory.instance.inventoryItemList[j].itemID)
                     {
                            Inventory.instance.DeleteItem(Inventory.instance.inventoryItemList[j]); // 재료 아이템 인벤토리에서 제거
                     }
                 }
            }
            craftingItemList.Clear(); // 크래프팅 아이템 리스트 초기화
            }
            else
            {
                Debug.Log("존재하지 않는 조합입니다.");
            }
        }
    }

    public void onClickGuideButton()
    {
        isGuideActivated = true;

        Inventory.instance.activated = false;
    }

    public void onClickBackButton()
    {
        isGuideActivated = false;

        Inventory.instance.activated = true;
    }

    public void InsertSlot() // 크래프팅 슬롯에 아이템 추가
    {
        if (craftingItemList.Count > 0)
        {
            craftingSlots[craftingItemList.Count - 1].Additem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot].CloneItem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]));
=======
 //           CheckCombination();
        }
    }

    public void InsertSlot()
    {
        if (craftingItemList.Count > 0)
        {
            craftingSlots[craftingItemList.Count - 1].Additem(Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot]);
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
        }
        return;
    }

<<<<<<< HEAD
    public void CheckItemAmount(int i) // 보유 수량 이상 추가 방지
=======
    public void CheckItemAmount(int i)
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    {
        if (craftingItemList[i].itemCount == Inventory.instance.inventoryItemList[i].itemCount)
        {
            Debug.Log("보유 수량 이상을 담을 수 없습니다.");
        }
    }

    public void SlotItem()
    {
<<<<<<< HEAD
        if (craftingItemList.Count == 2) // 2개의 아이템으로 조합을 시도하는 경우
=======
        if (craftingItemList.Count == 2)        // 2개의 아이템으로 조합을 시도하는 경우
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
        {
//            craftingItemList[0].itemCount == 
        }
    }

<<<<<<< HEAD
 
    public int CheckCombination()
    {
        int output = 0;

=======
 /*
    public int CheckCombination()
    {
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
        if (craftingItemList.Count == 1)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
<<<<<<< HEAD
                if (craftingItemList[0].itemID == craftingCombination.combinations[i].firstID &&
                    craftingItemList[0].itemCount == craftingCombination.combinations[i].firstCount)
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    output = craftingCombination.combinations[i].outputID;
                }
                else
                {
=======
                if (craftingItemList[0].itemID == craftingCombination.combinations[i].firstID)
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    return craftingCombination.combinations[i].outputID;
                }
                else
                {
                    Debug.Log("존재하지 않는 조합입니다.");
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
                    return 0;
                }
            }
        }
<<<<<<< HEAD
        else if (craftingItemList.Count == 2) // 여기서부터 수정 필요
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
                    output = craftingCombination.combinations[i].outputID;
                }
                else 
                { 
                    return 0;
=======
        else if (craftingItemList.Count == 2)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
                if ((craftingItemList[0].itemID == craftingCombination.combinations[i].firstID ||
                    craftingItemList[0].itemID == craftingCombination.combinations[i].secondID) &&
                        (craftingItemList[1].itemID == craftingCombination.combinations[i].firstID ||
                        craftingItemList[1].itemID == craftingCombination.combinations[i].secondID)) {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    return craftingCombination.combinations[i].outputID;
                }
                else
                {
                    Debug.Log("존재하지 않는 조합입니다.");
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
                }
            }
        }
        else if (craftingItemList.Count == 3)
        {
            for (int i = 0; i < craftingCombination.combinations.Count; i++)
            {
<<<<<<< HEAD
                if ((
                    ((craftingItemList[0].itemID == craftingCombination.combinations[i].firstID) &&
                    (craftingItemList[0].itemCount == craftingCombination.combinations[i].firstCount)) ||
                     (craftingItemList[0].itemID == craftingCombination.combinations[i].secondID) ||
                     (craftingItemList[0].itemID == craftingCombination.combinations[i].thirdID)) &&
                          (
                          (craftingItemList[1].itemID == craftingCombination.combinations[i].firstID) ||
                           (craftingItemList[1].itemID == craftingCombination.combinations[i].secondID) ||
                           (craftingItemList[1].itemID == craftingCombination.combinations[i].thirdID)) &&
                           (
                           (craftingItemList[2].itemID == craftingCombination.combinations[i].firstID) ||
                            (craftingItemList[2].itemID == craftingCombination.combinations[i].secondID) ||
                            (craftingItemList[2].itemID == craftingCombination.combinations[i].thirdID)))
                {
                    Debug.Log(craftingCombination.combinations[i].outputID);
                    output = craftingCombination.combinations[i].outputID;
                }
                else
                { 
                    return 0;
                }
            }
        }
        else
        {
            return 0;
        }

        return output;
    }

    public void CraftItem()
    {
        
=======
 //             if ((조건1||조건2||조건3)&&(조건4||조건5||조건6)&&(조건7||조건8||조건9))
            }
        }
    }
 */

    public void CraftItem()
    {

>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    }
}
