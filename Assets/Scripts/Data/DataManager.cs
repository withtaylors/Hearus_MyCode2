using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Item;
using System;

public class PlayerData
{
    public string filename;
    public int item;

    //플레이어 위치
    public float x = 27f;
    public float y = 31f;
    public float z = -73f;
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
    public bool isMeet;
    public bool isPicking;

    public InventoryData(int _itemID, string _itemName, string _itemDes, ItemType _itemType, ItemEffect _itemEffect, int _effectValue, int _itemCount = 1, bool _isMeet = false, bool _isPicking = false)   // 생성자
    {
        // itemID는 아이콘 파일의 이름과 같게 함
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
        itemEffect = _itemEffect;
        effectValue = _effectValue;
        isMeet = _isMeet;
        isPicking = _isPicking;
    }
}

[Serializable]
public class InventoryDataWrapper
{
    public List<InventoryData> items;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤패턴

    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성
    public PlayerData nowPlayerDefault = new PlayerData(); // 기본 플레이어 데이터 생성

    public InventoryDataWrapper dataWrapper = new InventoryDataWrapper();

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        // 기본 플레이어 위치 설정
        nowPlayerDefault.x = 27f;
        nowPlayerDefault.y = 31f;
        nowPlayerDefault.z = -73f;

        path = Application.persistentDataPath + "/save";	// 경로 지정
        print(path);
    }

    public void SaveData()
    {
        string playerData = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString() + "_player.json", playerData);

        dataWrapper.items = new List<InventoryData>();
        for (int i = 0; i < InventoryDataManager.Instance.inventoryItemList.Count; i++)
        {
            Item _item = InventoryDataManager.Instance.inventoryItemList[i];
            InventoryData _inventoryData = new InventoryData(_item.itemID, _item.itemName, _item.itemDescription,
                _item.itemType, _item.itemEffect, _item.effectValue, _item.itemCount, _item.isMeet, _item.isPicking);
            dataWrapper.items.Add(_inventoryData);
            Debug.Log(_inventoryData.itemID);
        }
        string inventoryData = JsonUtility.ToJson(dataWrapper);
        File.WriteAllText(path + nowSlot.ToString() + "_inventory.json", inventoryData);
    }

    public void LoadData()
    {
        string playerData = File.ReadAllText(path + nowSlot.ToString() + "_player.json");
        nowPlayer = JsonUtility.FromJson<PlayerData>(playerData);

        string inventoryData = File.ReadAllText(path + nowSlot.ToString() + "_inventory.json");
        InventoryDataWrapper dataWrapper = JsonUtility.FromJson<InventoryDataWrapper>(inventoryData);

        if (dataWrapper != null && dataWrapper.items != null)
        {
            List<InventoryData> itemsToLoad = dataWrapper.items;

            if (itemsToLoad.Count > 0)
                for (int i = 0; i < itemsToLoad.Count; i++)
                {
                    Item _item = new Item(itemsToLoad[i].itemID, itemsToLoad[i].itemName, itemsToLoad[i].itemDescription,
                    itemsToLoad[i].itemType, itemsToLoad[i].itemEffect, itemsToLoad[i].effectValue, itemsToLoad[i].itemCount,
                    itemsToLoad[i].isMeet, itemsToLoad[i].isPicking);

                    InventoryDataManager.Instance.inventoryItemList.Add(_item);
                }
        }
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
        InventoryDataManager.Instance.inventoryItemList.Clear();
    }
}