using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
// 인벤토리 전반적인 기능을 구현한 스크립트.

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public ItemDatabase theDatabase;       // 아이템 데이터베이스 객체

    private List<InventorySlot> slots;      // 인벤토리 슬롯들
    public Transform tf;                    // 슬롯들의 부모 객체 (GridSlot)

    private ItemDescription itemDes;
    public Transform tf_itemDes;

    public List<Item> inventoryItemList;   // 플레이어가 소지한 아이템 리스트

    public GameObject slotPrefab;           // 슬롯 동적 생성을 위해 슬롯 프리팹 불러오기
    public GameObject go_Inventory;         // 인벤토리 활성화/비활성화를 위해 GameObject 불러오기
    public GameObject go_itemDes;           // 설명 패널 활성화/비활성화를 위해 GameObject 불러오기
    public GameObject go_SelectedImage;     // 선택된 슬롯이라는 것을 나타내는 이미지 불러오기
    public GameObject go_Page1;
<<<<<<< HEAD
    public GameObject go_TapPanel;

    public int selectedSlot;                // 선택된 슬롯 -> 기본은 0

    public bool activated;                 // 인벤토리 활성화 시 true
=======

    public int selectedSlot;                // 선택된 슬롯 -> 기본은 0

    private bool activated;                 // 인벤토리 활성화 시 true
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    private bool stopKeyInput;              // 키 입력 제한 (소비할 때, 제거할 때 팝업 메시지 뜨면 키 입력 금지)

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theDatabase = FindObjectOfType<ItemDatabase>();                 // 아이템 데이터베이스 오브젝트
        inventoryItemList = new List<Item>();                           // 인벤토리 아이템 리스트
        slots = new List<InventorySlot>(tf.GetComponentsInChildren<InventorySlot>());
        itemDes = tf_itemDes.GetComponentInChildren<ItemDescription>();
        selectedSlot = 0;

        RectTransform rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!stopKeyInput)
        {
            /*          if (Input.GetKeyDown(KeyCode.I)) // 인벤토리 활성화
                        {
                            activated = !activated;
                            if (activated)
                            {
                                go_Inventory.SetActive(true);
                            }
                            else
                            {
                                go_Inventory.SetActive(false);
                            }
                        } */
            if (go_Page1.activeSelf)        // 설정 패널 내 인벤토리 페이지 선택 여부
            {
                activated = true;
                go_Inventory.SetActive(true);
            }
<<<<<<< HEAD
            else if (!go_Page1.activeSelf)
            {
                activated = false;
                go_Inventory.SetActive(false);
            }

            if (!go_TapPanel.activeSelf)
=======
            else
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
            {
                activated = false;
                go_Inventory.SetActive(false);
            }
<<<<<<< HEAD

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
            if (activated)
            {
                if (inventoryItemList.Count > 0) // 인벤토리 아이템 리스트에 저장된 요소가 있을 경우에만 실행
                {
                    go_SelectedImage.SetActive(true);
                    go_itemDes.SetActive(true);
                    TryInputArrowKey();
                    SelectedItemDes(inventoryItemList[selectedSlot].itemID);
                }
                else
                {
                    go_SelectedImage.SetActive(false);
                    go_itemDes.SetActive(false);
                }
            }
        }
    }
    public void GetAnItem(int _itemID, int _count = 1)              // 인벤토리 리스트에 아이템 추가
    {
        for (int i = 0; i < theDatabase.itemList.Count; i++)        // 데이터베이스 아이템 검색
        {
            if (_itemID == theDatabase.itemList[i].itemID)          // 데이터베이스 아이템 발견
            {
                for (int j = 0; j < inventoryItemList.Count; j++)   // 소지품 중 같은 아이템이 있는지 검색
                {
                    if (inventoryItemList[j].itemID == _itemID)     // 같은 아이템이 있다면 개수만 증가
                    {
                        // inventoryItemList[j].itemCount += _count;
<<<<<<< HEAD
                        slots[j].IncreaseCount(inventoryItemList[j]);
=======
                        slots[j].setItemCount(inventoryItemList[j]);
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
                        return;
                    }
                }
                inventoryItemList.Add(theDatabase.itemList[i]);     // 없다면 소지품에 해당 아이템 추가
                CreateSlot();
                slots[slots.Count - 1].Additem(theDatabase.itemList[i]);
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID 값을 가진 아이템이 존재하지 않습니다.");
    }
    public void CreateSlot()        // 새로운 아이템의 경우 새로운 슬롯 생성
    {
        InventorySlot slot = Instantiate(slotPrefab, tf).GetComponent<InventorySlot>();
        slots.Add(slot);
        
//        Button button = slot.GetComponentInChildren<Button>();
//        SlotButton slotButton = button.GetComponentInChildren<SlotButton>();
//        slotButton.slotIndex = slots.Count - 1; // 슬롯 인덱스 설정
    }
    private void SelectedSlot()     // 슬롯이 선택되었음을 나타내는 이미지(테두리) 위치 이동
    {
<<<<<<< HEAD
        if (inventoryItemList.Count > 0)
        {
            go_SelectedImage.transform.position = slots[selectedSlot].transform.position;
        }
        else
        {
            Debug.Log("인벤토리에 항목이 존재하지 않습니다.");
        }

=======
        go_SelectedImage.transform.position = slots[selectedSlot].transform.position;
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    }
    private void TryInputArrowKey() // 키보드로 선택된 슬롯 변경
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSlotLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSlotRight();
        }
        SelectedSlot();
    }
    private void ChangeSlotLeft()   // 왼쪽 슬롯으로 이동 (인벤토리 아이템 리스트의 범위 내에서만 작동)
    {
            if (selectedSlot == 0)
                selectedSlot = inventoryItemList.Count - 1;
            else
                selectedSlot--;
    }
    private void ChangeSlotRight()  // 오른쪽 슬롯으로 이동 (인벤토리 아이템 리스트의 범위 내에서만 작동)
    {
            if (selectedSlot == inventoryItemList.Count - 1)
                selectedSlot = 0;
            else
                selectedSlot++;
    }
    private void SelectedItemDes(int _itemID)                   // 선택된 아이템의 설명을 보여 줌
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i].itemID == _itemID)
            {
                itemDes.DisplayDes(inventoryItemList[i]);
                break;
            }
        }
    }
<<<<<<< HEAD
    public void DeleteItem(Item _item)    // 아이템 제거
    {        
        if (inventoryItemList.Count == 0) // 인벤토리가 비어 있으면 함수 즉시 종료
            return;

        int slotIndex = 0;

        for (int i = 0; i < inventoryItemList.Count; i++) // 매개 변수로 받은 아이템이 몇 번째 슬롯에 존재하는지 검사
        {
            if (inventoryItemList[i].itemID == _item.itemID)
            {
                slotIndex = i;
            }
        }
        inventoryItemList.Remove(_item); // 인벤토리 아이템 리스트에서 제거
        Destroy(slots[slotIndex].gameObject); // slotIndex번째 슬롯 오브젝트 제거
        slots.RemoveAt(slotIndex); // slot 리스트에서 slotIndex번째 요소 제거

        selectedSlot = 0;

        SelectedSlot();
    }
=======
    public void DeleteItem()        // 아이템 제거
    {
        int temp = selectedSlot;          // selectedSlot 임시 저장

        inventoryItemList.RemoveAt(temp);
        Destroy(slots[temp].gameObject);
        slots.RemoveAt(temp);

        if (selectedSlot >= inventoryItemList.Count)
        {
            selectedSlot = inventoryItemList.Count - 1;
        }
        
        if (selectedSlot < 0 && inventoryItemList.Count > 0)
        {
            selectedSlot = 0;
        }

        if (inventoryItemList.Count == 0) // 인벤토리가 비어 있으면 함수 즉시 종료
            return;

        SelectedSlot();
    }

>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
}
