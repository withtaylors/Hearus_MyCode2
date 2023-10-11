using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Item;
using System;
using System.Linq;

[Serializable]
public class PlayerData
{
    public string filename;
    public bool firstStart = true;

    public bool gameNext = true;
    public bool gameBefore = false;
    public bool isFinishedTutorial = false;
    public string currentMap = "태초의숲";

    //플레이어 위치
    public float x = 40f;
    public float y = 10f;
    public float z = 0.5f;

    public string nowCharacter = "None";
    public int frithIntimacy = 0;
    public int playerHP = 100;
}

[Serializable]
public class InventoryData
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;
    public ItemEffect itemEffect;
    public int effectValue;
    public bool isCountable;
    public bool isMeet;
    public bool isPicking;

    public InventoryData(int _itemID, string _itemName, string _itemDes, ItemType _itemType, ItemEffect _itemEffect, 
        int _effectValue, int _itemCount = 1, bool _isCountable = true, bool _isMeet = false, bool _isPicking = false)
    {
        this.itemID = _itemID;
        this.itemName = _itemName;
        this.itemDescription = _itemDes;
        this.itemType = _itemType;
        this.itemCount = _itemCount;
        this.itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
        this.itemEffect = _itemEffect;
        this.effectValue = _effectValue;
        this.isCountable = _isCountable;
        this.isMeet = _isMeet;
        this.isPicking = _isPicking;
    }
}

[Serializable]
public class InventoryDataWrapper
{
    public List<InventoryData> items;
    public List<int> fieldItemIDList;
    public List<int> getItemIDList;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤패턴
    
    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성
    public InventoryDataWrapper dataWrapper = new InventoryDataWrapper();

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호
    public int firstSlot; // 현재 슬롯번호

    public int selectedSlot;
    public event Action<int> OnSelectedSlotChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";	// 경로 지정
        print(path);

        dataWrapper.items = new List<InventoryData>();
        dataWrapper.fieldItemIDList = new List<int>();
        dataWrapper.getItemIDList = new List<int>();
    }

    //SetSelectedSlot 함수 추가
    public void SetSelectedSlot(int slotNumber)
    {
        selectedSlot = slotNumber;
        OnSelectedSlotChanged?.Invoke(selectedSlot);
    }

    public void SaveData(int slotNumber)
    {
        nowSlot = slotNumber;

        if (File.Exists(path + $"{nowSlot}_player.json")) // 파일이 이미 있는 경우
        {
            // 기존 파일에서 'filename' 속성만 가져옴
            string existingFileData = File.ReadAllText(path + $"{nowSlot}_player.json");
            PlayerData existingPlayerData = JsonUtility.FromJson<PlayerData>(existingFileData);

            // 현재 파일에 'filename'을 적용하고 저장
            nowPlayer.filename = existingPlayerData.filename;
        }

        SaveInventoryData();

        string playerData = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + $"{nowSlot}_player.json", playerData);

        string inventoryData = JsonUtility.ToJson(dataWrapper);
        File.WriteAllText(path + $"{nowSlot}_inventory.json", inventoryData);
    }

    public void SaveInventoryData()
    {
        dataWrapper.items.Clear(); // 초기화
        dataWrapper.items = new List<InventoryData>(); // 생성

        for (int i = 0; i < InventoryDataManager.Instance.inventoryItemList.Count; i++) // 인벤토리 데이터 매니저에 있는 아이템 데이터들을 dataWrapper로 옮김
        {
            Item _item = InventoryDataManager.Instance.inventoryItemList[i];
            InventoryData _inventoryData = new InventoryData(_item.itemID, _item.itemName, _item.itemDescription,
                _item.itemType, _item.itemEffect, _item.effectValue, _item.itemCount, _item.isCountable, _item.isMeet, _item.isPicking);
            dataWrapper.items.Add(_inventoryData);
        }

        dataWrapper.fieldItemIDList.Clear();
        dataWrapper.fieldItemIDList = new List<int>();

        for (int i = 0; i < InventoryDataManager.Instance.fieldItemIDList.Count; i++)
            dataWrapper.fieldItemIDList.Add(InventoryDataManager.Instance.fieldItemIDList[i]);

        dataWrapper.getItemIDList.Clear();
        dataWrapper.getItemIDList = new List<int>();

        for (int i = 0; i < InventoryDataManager.Instance.getItemIDList.Count; i++)
            dataWrapper.getItemIDList.Add(InventoryDataManager.Instance.getItemIDList[i]);
    }

    public void LoadData()
    {
        string playerData = File.ReadAllText(path + nowSlot.ToString() + "_player.json");
        nowPlayer = JsonUtility.FromJson<PlayerData>(playerData);
            
        string inventoryData = File.ReadAllText(path + nowSlot.ToString() + "_inventory.json");
        dataWrapper = JsonUtility.FromJson<InventoryDataWrapper>(inventoryData);
    }

    public void LoadInventory()
    {
        Debug.Log("LoadInventory()");
        if (dataWrapper != null && dataWrapper.items != null) // dataWrapper가 null이 아니라면
        { 
            List<InventoryData> itemsToLoad = dataWrapper.items;

            if (itemsToLoad.Count > 0) // dataWrapper에 있는 아이템 데이터들을 인벤토리 데이터 매니저로 옮김
                for (int i = 0; i < itemsToLoad.Count; i++)
                {
                    Item _item = new Item(itemsToLoad[i].itemID, itemsToLoad[i].itemName, itemsToLoad[i].itemDescription,
                    itemsToLoad[i].itemType, itemsToLoad[i].itemEffect, itemsToLoad[i].effectValue, itemsToLoad[i].isCountable,
                    itemsToLoad[i].itemCount, itemsToLoad[i].isMeet, itemsToLoad[i].isPicking);

                    InventoryDataManager.Instance.inventoryItemList.Add(_item);
                }

            InventoryDataManager.Instance.fieldItemIDList = dataWrapper.fieldItemIDList.ToList();
            InventoryDataManager.Instance.getItemIDList = dataWrapper.getItemIDList.ToList();
        }
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();

        dataWrapper.items.Clear();
    }

    public void InventoryClear()
    {
        InventoryDataManager.Instance.inventoryItemList.Clear();
    }

    public void FieldDataClear()
    {
        dataWrapper.fieldItemIDList.Clear();
        dataWrapper.getItemIDList.Clear();
    }
}