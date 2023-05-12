using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{    
    // 아이템 데이터베이스
    public static ItemDatabase instance;

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

    public List<Item> itemList = new List<Item>();
    void Start()
    {
        itemList.Add(new Item(100, "상자", "그냥 상자다.", Item.ItemType.소모품));
        itemList.Add(new Item(101, "쿠키", "내가 만든 쿠키 너를 위해 구웠지 but you know that it ain't for free, yeah 내가 만든 쿠키 너무 부드러우니 (what?) 자꾸만 떠오르니 (ayy)", Item.ItemType.소모품));
    } 
}