using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private ItemDatabase theDatabase;       // 아이템 데이터베이스 객체

    private List<InventorySlot> slots;      // 인벤토리 슬롯들
    public Transform tf;                    // 슬롯들의 부모 객체 (GridSlot)

    private ItemDescription itemDes;
    public Transform tf_itemDes;

    private List<Item> inventoryItemList;   // 플레이어가 소지한 아이템 리스트

    public GameObject slotPrefab;           // 슬롯 동적 생성을 위해 슬롯 프리팹 불러오기
    public GameObject go_Inventory;         // 인벤토리 활성화/비활성화를 위해 GameObject 불러오기
    public GameObject go_SelectedImage;     // 선택된 슬롯이라는 것을 나타내는 이미지 불러오기

    public Button button;                   // 클릭으로도 슬롯을 선택할 수 있도록 하는 버튼

    public int selectedSlot;                // 선택된 슬롯 -> 기본은 0

    private bool activated;                 // 인벤토리 활성화 시 true
    private bool stopKeyInput;              // 키 입력 제한 (소비할 때, 제거할 때 팝업 메시지 뜨면 키 입력 금지)
    private bool preventMove;
    private bool preventExec;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        // 싱글톤
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 유지
    }

    void Start()
    {
        theDatabase = FindObjectOfType<ItemDatabase>();                 // 아이템 데이터베이스 오브젝트
        inventoryItemList = new List<Item>();                           // 인벤토리 아이템 리스트
        slots = new List<InventorySlot>(tf.GetComponentsInChildren<InventorySlot>());
        itemDes = tf_itemDes.GetComponentInChildren<ItemDescription>();
        selectedSlot = 0;
    }

    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I)) // 인벤토리 활성화
            {
                activated = !activated;
                if (activated)
                {
                    go_Inventory.SetActive(true);
                    preventExec = true;
                    GameObject.FindObjectOfType<Player>().speed = 0; // 인벤토리 활성화 시 플레이어 속도 0
                }
                else
                {
                    go_Inventory.SetActive(false);
                    GameObject.FindObjectOfType<Player>().speed = 3; // 인벤토리 비활성화 시 플레이어 속도 원상 복구 (속도 수정 필요)
                    preventExec = false;
                }
            }
            if (activated)
            {
                if (inventoryItemList.Count > 0) // 인벤토리 아이템 리스트에 저장된 요소가 있을 경우에만 실행
                {
                    go_SelectedImage.SetActive(true);
                    TryInputArrowKey();
                    SelectedItemDes(inventoryItemList[selectedSlot].itemID);
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
                        slots[j].setItemCount(inventoryItemList[j]);
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
    }
    private void SelectedSlot()     // 슬롯이 선택되었음을 나타내는 이미지(테두리) 위치 이동
    {
        go_SelectedImage.transform.position = slots[selectedSlot].transform.position;
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
                itemDes.DisplayDes(inventoryItemList[i]);
            break;
        }

    }
    private void PreventMove()
    {
        if (preventMove == true)
            GameObject.FindObjectOfType<Player>().speed = 0;
        else
            GameObject.FindObjectOfType<Player>().speed = 3;
    }
}
