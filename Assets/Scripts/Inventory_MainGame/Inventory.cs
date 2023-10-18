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
    public ItemPickup[] go_Item;
    private InventoryDataManager inventoryDataManager;

    public List<InventorySlot> slots;                         // 인벤토리 슬롯들
    [SerializeField] private Transform tf;                    // 슬롯들의 부모 객체 (GridSlot) -> 슬롯들은 tf 밑에 생성되어야 하므로 부모 객체를 선언함
    [SerializeField] private ItemDescription itemDes;         // 아이템 설명 패널

    public List<Item> inventoryItemList;   // 플레이어가 소지한 아이템 리스트
    public List<int> fieldItemIDList;
    public List<int> getItemIDList;

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
        inventoryDataManager = FindObjectOfType<InventoryDataManager>();

        inventoryItemList = new List<Item>();                                           // 인벤토리 아이템 리스트
        slots = new List<InventorySlot>(tf.GetComponentsInChildren<InventorySlot>());   // 슬롯 리스트 초기화

        go_Item = FindObjectsOfType<ItemPickup>();

        if (InventoryDataManager.Instance.fieldItemIDList.Count > 0)
            LoadFieldData();

        if (InventoryDataManager.Instance.inventoryItemList.Count > 0)
            LoadInventory();
        else
            Debug.Log("Inventory None");

        selectedSlot = 0;                                                               // 현재 선택된 슬롯을 나타내는 값, 디폴트는 0
        StartCoroutine(UpdateSlot());
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

    private IEnumerator UpdateSlot()
    {
        while (true)
        {
            if (activated)
            {
                SelectedSlot();
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.01f);
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
                    if (inventoryItemList[j].itemID == _itemID)     // 셀 수 있는 아이템이고, 같은 아이템이 있다면 개수만 증가
                    {
                        if (inventoryItemList[j].isCountable == true)
                        {
                            slots[j].IncreaseCount(inventoryItemList[j]);
                            return;
                        }
                    }
                }
                inventoryItemList.Add(ItemDatabase.itemList[i]);            // 같은 아이템이 없다면/셀 수 없는 아이템이라면 슬롯 추가
                CreateSlot();                                               // 슬롯 생성
                slots[slots.Count - 1].Additem(ItemDatabase.itemList[i]);   // 슬롯에 아이템 넣기
                slots[slots.Count - 1].UncountableItem(ItemDatabase.itemList[i]);
                return;
            }
        }
        Debug.LogError("데이터베이스에 해당 ID 값을 가진 아이템이 존재하지 않습니다."); // 데이터베이스에 해당하는 아이템 ID가 존재하지 않는 경우
    }

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

    public void LoadInventory()
    {
        for (int i = 0; i < InventoryDataManager.Instance.inventoryItemList.Count; i++)
        {
            inventoryItemList.Add(InventoryDataManager.Instance.inventoryItemList[i]);
            CreateSlot();
            slots[slots.Count - 1].Additem(InventoryDataManager.Instance.inventoryItemList[i]);   // 슬롯에 아이템 넣기
            slots[slots.Count - 1].UncountableItem(InventoryDataManager.Instance.inventoryItemList[i]);
        }
    }

    public void LoadFieldData()
    {
        for (int i = 0; i < InventoryDataManager.Instance.fieldItemIDList.Count; i++)
        {
            for (int j = 0; j < go_Item.Length; j++)
            {
                if (go_Item[j]._fieldItemID == InventoryDataManager.Instance.fieldItemIDList[i])
                    go_Item[j].gameObject.SetActive(false);
            }

            fieldItemIDList.Add(InventoryDataManager.Instance.fieldItemIDList[i]);
        }

        for (int i = 0; i < InventoryDataManager.Instance.getItemIDList.Count; i++)
        {
            getItemIDList.Add(InventoryDataManager.Instance.getItemIDList[i]);
        }
    }

    // CreateSlot(): 인벤토리 아이템 리스트에 새로운 아이템이 들어왔을 경우, 새로운 슬롯을 생성하는 메소드
    public void CreateSlot()
    {
        InventorySlot slot = Instantiate(slotPrefab, tf).GetComponent<InventorySlot>(); // 슬롯 프리팹을 복사해 tf의 자식으로 넣음
        slots.Add(slot);                                                                // 복사한 슬롯을 슬롯 리스트에도 추가
        
        slot.slotIndex = slots.Count - 1; // 슬롯 인덱스 설정
    }

    // SelectedSlot(): 슬롯이 선택되었음을 나타내는 이미지(테두리) 위치를 이동시키는 메소드
    public void SelectedSlot()
    {
        if (slots.Count > 0)
            go_SelectedImage.transform.position = slots[selectedSlot].transform.position; // 위치 변경
    }

    // TryInputArrowKey(): // 좌우 화살표 키로 선택된 슬롯을 변경하는 메소드
    private void TryInputArrowKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSlotLeft();
            SelectedSlot(); // selectedSlot 위치 업데이트
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSlotRight();
            SelectedSlot(); // selectedSlot 위치 업데이트
        }
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
                    if (inventoryItemList[i].itemCount > 1) // 해당 아이템의 개수가 1개를 초과할 경우 => 아이템의 개수만 감소
                    {
                        inventoryItemList[i].itemCount -= 1;
                        slots[i].UpdateItemCount(inventoryItemList[i]);
                        break;
                    }
                    else if (inventoryItemList[i].itemCount == 1) // 해당 아이템의 개수가 1개일 경우 => 슬롯 삭제
                    {
                        inventoryItemList.RemoveAt(i); // 인벤토리 아이템 리스트에서 삭제
                        Destroy(slots[i].gameObject); // 슬롯 오브젝트 삭제
                        slots.RemoveAt(i); // 슬롯 리스트에서 삭제
                        SetSlotIndex(); // 슬롯 인덱스 재조정
                        selectedSlot = 0;
                        SelectedSlot();
                        break;
                    }
                }
            }
        }
        if (inventoryItemList.Count == 0) // 인벤토리가 비어 있으면 함수 즉시 종료
            return;
    }

    private void SetSlotIndex() // 슬롯의 인덱스를 재조정하는 함수. 슬롯이 삭제될 경우 호출됨.
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].slotIndex = i;
    }

    public void SaveInventoryDataManager()
    {
        InventoryDataManager.Instance.inventoryItemList.Clear();
        for (int i = 0; i < inventoryItemList.Count; i++)
            InventoryDataManager.Instance.inventoryItemList.Add(inventoryItemList[i]);
    }

    public void SaveFieldDataManager()
    {
        InventoryDataManager.Instance.fieldItemIDList.Clear();
        InventoryDataManager.Instance.getItemIDList.Clear();

        for (int i = 0; i < fieldItemIDList.Count; i++)
            InventoryDataManager.Instance.fieldItemIDList.Add(fieldItemIDList[i]);

        for (int i = 0; i < getItemIDList.Count; i++)
            InventoryDataManager.Instance.getItemIDList.Add(getItemIDList[i]);
    }

    public void SaveFieldData(int _fieldItemID, int _getItemID)
    {
        fieldItemIDList.Add(_fieldItemID); // 획득한 오브젝트 추가

        for (int i = 0; i < getItemIDList.Count; i++) // 아이템 이미 획득한 적 있다면 return
            if (_getItemID == getItemIDList[i])
                return;

        getItemIDList.Add(_getItemID); // 없으면 추가
    }
}
