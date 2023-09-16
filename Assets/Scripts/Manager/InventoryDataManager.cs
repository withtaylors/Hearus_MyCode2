using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager : MonoBehaviour
{
    public static InventoryDataManager Instance;

    public List<Item> inventoryItemList;
    public List<int> fieldItemIDList;

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
    }
}