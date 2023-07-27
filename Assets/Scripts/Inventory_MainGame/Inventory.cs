using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public static ItemDatabase theDatabase; // 아이템 데이터베이스 객체

    private List<InventorySlot> slots;                        // 인벤토리 슬롯들
    [SerializeField] private Transform tf;                    // 슬롯들의 부모 객체 (GridSlot) -> 슬롯들은 tf 밑에 생성되어야 하므로 부모 객체를 선언함
    [SerializeField] private ItemDescription itemDes;         // 아이템 설명 패널

    public List<Item> inventoryItemList;   // 플레이어가 소지한 아이템 리스트

    public GameObject slotPrefab;           // 슬롯 동적 생성을 위해 슬롯 프리팹 불러오기
    public GameObject go_Inventory;         // 인벤토리 활성화/비활성화를 위해 GameObject 불러오기
    public GameObject go_itemDes;           // 설명 패널 활성화/비활성화를 위해 GameObject 불러오기
    public GameObject go_SelectedImage;     // 선택된 슬롯이라는 것을 나타내는 이미지 불러오기
    public GameObject go_SettingPanel;      // Setting Panel 비활성화 시 인벤토리도 비활성화시키기 위함
    public GameObject go_Page1;             // 다른 페이지도 넘어갈 시 인벤토리를 비활성화시키기 위함

    public int selectedSlot;                // 선택된 슬롯 -> 기본은 0

    public bool activated;                  // 인벤토리 활성화 시 true
    private bool stopKeyInput;              // 키 입력 제한 (소비할 때, 제거할 때 팝업 메시지 뜨면 키 입력 금지)

    private void Awake()
    {
        instance = this; // 싱글톤
    }

    void Start()
    {
        theDatabase = FindObjectOfType<ItemDatabase>();                                 // 아이템 데이터베이스 오브젝트
        inventoryItemList = new List<Item>();                                           // 인벤토리 아이템 리스트
        slots = new List<InventorySlot>(tf.GetComponentsInChildren<InventorySlot>());   //
        selectedSlot = 0;                                                               // 현재 선택된 슬롯을 나타내는 값, 디폴트는 0
        if (InventoryDataManager.Instance.inventoryItemList.Count > 0)
        {
            for (int i = 0; i < InventoryDataManager.Instance.inventoryItemList.Count; i++)
                LoadItem(InventoryDataManager.Instance.inventoryItemList[i].itemID, InventoryDataManager.Instance.inventoryItemList[i].itemCount);
        }
    }

    void Update()
    {
        if (!stopKeyInput)
        {
            if (go_Page1.activeSelf)         // 설정 패널 내 인벤토리 페이지 선택 여부
                activated = true;
            else
                activated = false;

            if (!go_SettingPanel.activeSelf) // 설정 패널 활성화 여부
                activated = false;

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

                go_Inventory.SetActive(true);
            }
            else
            {
                go_Inventory.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        IsPicking();
    }

    // GetAnItemYarn(int): Yarn Script(스크립트 및 선택지 관리 API)에서 호출하기 위한 정적 메소드
    [YarnCommand("getAnItem")]
    public static void GetAnItemYarn(int _itemID)
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (ItemDatabase.itemList[i].itemID == _itemID)         // 아이템 데이터베이스에 해당 아이템 ID 검색
            {
                ItemDatabase.itemList[i].isMeet = true;             // isMeet: 이전에 발견했던 아이템인지
                ItemDatabase.itemList[i].isPicking = true;          // isPicking: isPicking() 메소드에서 어떤 아이템을 습득시켜야 할지 전달
            }
        }
    }

    // IsPicking(): GetAnItemYarn() 메소드에서 GetAnItem() 메소드를 바로 호출할 수 없으므로 중간 다리 역할을 하는 메소드
    private void IsPicking()
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (ItemDatabase.itemList[i].isPicking == true)         // isPicking == true인 아이템이 있다면
            {
                GetAnItem(ItemDatabase.itemList[i].itemID);         // GetAnItem() 메소드로 아이템 ID 전달
                ItemDatabase.itemList[i].isPicking = false;         // isPicking == false로 전환
            }
        }
    }


    // GetAnItem(int, int): 인벤토리 아이템 리스트에 아이템을 실질적으로 추가시키는 메소드
    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)       // 데이터베이스에 매개변수로 들어온 아이템 ID 검색
        {
            if (_itemID == ItemDatabase.itemList[i].itemID)         // 데이터베이스에 해당하는 아이템 ID가 존재하는 경우,
            {
                for (int j = 0; j < inventoryItemList.Count; j++)   // 먼저 소지품 중 같은 아이템이 있는지 검색
                {
                    if (inventoryItemList[j].itemID == _itemID)     // 같은 아이템이 있다면 개수만 증가
                    {
                        slots[j].IncreaseCount(inventoryItemList[j]);
                        for (int k = 0; k < InventoryDataManager.Instance.inventoryItemList.Count; k++)
                        {
                            if (InventoryDataManager.Instance.inventoryItemList[k].itemID == _itemID)
                            {
                                InventoryDataManager.Instance.inventoryItemList[k].itemCount += 1;
                                break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        inventoryItemList.Add(ItemDatabase.itemList[i]);            // 없다면 인벤토리 아이템 리스트에 해당 아이템 추가
                        CreateSlot();                                               // 슬롯 생성
                        slots[slots.Count - 1].Additem(ItemDatabase.itemList[i]);   // 슬롯에 아이템 넣기
                        InventoryDataManager.Instance.inventoryItemList.Add(ItemDatabase.itemList[i]);
                        //ItemDatabase.itemList[i].isPicking = false;
                        return;
                    }
                }
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID 값을 가진 아이템이 존재하지 않습니다."); // 데이터베이스에 해당하는 아이템 ID가 존재하지 않는 경우
    }

    /*
    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if (inventoryItemList[i].itemID == _itemID)
            {
                slots[i].IncreaseCount(inventoryItemList[i]);
                for (int j = 0; j < InventoryDataManager.Instance.inventoryItemList.Count; j++)
                {
                    if (InventoryDataManager.Instance.inventoryItemList[j].itemID == _itemID)
                    {
                        InventoryDataManager.Instance.inventoryItemList[j].itemCount += 1;
                        break;
                    }
                }
                return;
            }
            else
            {
                inventoryItemList.Add(ItemDatabase.itemList[i]);            // 없다면 인벤토리 아이템 리스트에 해당 아이템 추가
                CreateSlot();                                               // 슬롯 생성
                slots[slots.Count - 1].Additem(ItemDatabase.itemList[i]);   // 슬롯에 아이템 넣기
                InventoryDataManager.Instance.inventoryItemList.Add(ItemDatabase.itemList[i]);
                //ItemDatabase.itemList[i].isPicking = false;
                return;
            }
        }
    }
    */

    public void LoadItem(int _itemID, int _count)
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (_itemID == ItemDatabase.itemList[i].itemID)
            {
                if (_count > 1)
                {
                    inventoryItemList.Add(ItemDatabase.itemList[i]);
                    CreateSlot();
                    slots[slots.Count - 1].Additem(ItemDatabase.itemList[i]);
                    for (int j = 0; j < _count - 1; j++)
                        slots[slots.Count - 1].IncreaseCount(ItemDatabase.itemList[i]);
                }
                else if (_count == 1)
                {
                    inventoryItemList.Add(ItemDatabase.itemList[i]);
                    CreateSlot();
                    slots[slots.Count - 1].Additem(ItemDatabase.itemList[i]);
                }
                else
                {
                    Debug.Log("유효하지 않은 값입니다.");
                    return;
                }
            }
        }
    }

    // CreateSlot(): 인벤토리 아이템 리스트에 새로운 아이템이 들어왔을 경우, 새로운 슬롯을 생성하는 메소드
    public void CreateSlot()
    {
        InventorySlot slot = Instantiate(slotPrefab, tf).GetComponent<InventorySlot>(); // 슬롯 프리팹을 복사해 tf의 자식으로 넣음
        slots.Add(slot);                                                                // 복사한 슬롯을 슬롯 리스트에도 추가
        
//        Button button = slot.GetComponentInChildren<Button>();
//        SlotButton slotButton = button.GetComponentInChildren<SlotButton>();
//        slotButton.slotIndex = slots.Count - 1; // 슬롯 인덱스 설정
    }

    // SelectedSlot(): 슬롯이 선택되었음을 나타내는 이미지(테두리) 위치를 이동시키는 메소드
    private void SelectedSlot()
    {
        go_SelectedImage.transform.position = slots[selectedSlot].transform.position; // 위치 변경
    }

    // TryInputArrowKey(): // 좌우 화살표 키로 선택된 슬롯을 변경하는 메소드
    private void TryInputArrowKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeSlotLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeSlotRight();

        SelectedSlot(); // selectedSlot 위치 업데이트
    }

    private void ChangeSlotLeft()   // 왼쪽 슬롯으로 이동 (인벤토리 아이템 리스트의 범위 내에서만 작동)
    {
            if (selectedSlot == 0)                          // 첫 번째 슬롯에서 왼쪽 화살표 키를 누르면
                selectedSlot = inventoryItemList.Count - 1; // 마지막 슬롯으로 이동함
            else
                selectedSlot--;                             // 그렇지 않으면 그냥 왼쪽으로 한 번 이동
    }

    private void ChangeSlotRight()  // 오른쪽 슬롯으로 이동 (인벤토리 아이템 리스트의 범위 내에서만 작동)
    {
            if (selectedSlot == inventoryItemList.Count - 1) // 마지막 슬롯에서 오른쪽 화살표 키를 누르면
                selectedSlot = 0;                            // 첫 번째 슬롯으로 이동함
            else
                selectedSlot++;                              // 그렇지 않으면 그냥 오른쪽으로 한 번 이동
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

    public void DeleteItem(int _itemID)        // 아이템 제거
    {
        if (inventoryItemList.Count > 0)
        {
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (_itemID == inventoryItemList[i].itemID)
                {
                    if (inventoryItemList[i].itemCount > 1)
                    {
                        inventoryItemList[i].itemCount -= 1;
                        slots[i].UpdateItemCount(inventoryItemList[i]);
                        break;
                    }
                    else if (inventoryItemList[i].itemCount == 1)
                    {
                        inventoryItemList.RemoveAt(i);
                        Destroy(slots[i].gameObject);
                        slots.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        if (inventoryItemList.Count == 0) // 인벤토리가 비어 있으면 함수 즉시 종료
            return;

        selectedSlot = 0;

        SelectedSlot();
    }

}
