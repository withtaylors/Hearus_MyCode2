using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : MonoBehaviour
{
    public static InventoryDataManager Instance;

    public List<Item> inventoryItemList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        inventoryItemList = new List<Item>();
    }

    public void SaveFieldData(int _fieldItemID, int _getItemID)
    {
        DataManager.instance.dataWrapper.fieldItemIDList.Add(_fieldItemID); // 획득한 오브젝트 추가

        for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++) // 아이템 이미 획득한 적 있다면 return
            if (_getItemID == DataManager.instance.dataWrapper.getItemIDList[i])
                return;

        DataManager.instance.dataWrapper.getItemIDList.Add(_getItemID); // 없으면 추가
    }
}