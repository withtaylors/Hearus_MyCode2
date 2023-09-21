using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : MonoBehaviour
{
    public static InventoryDataManager Instance;

    public List<Item> inventoryItemList;
    public List<int> fieldItemIDList;
    public List<int> getItemIDList;

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
        fieldItemIDList = new List<int>();
        getItemIDList = new List<int>();
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