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

    public static List<Item> itemList = new List<Item>();

    void Start()
    {
        itemList.Add(new Item(101, "질긴 덩굴", "질긴 덩굴. 잘라내기는 어렵지만 그만큼 질기고 잘 끊어지지 않는다.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(102, "밧줄", "밧줄이다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0, 10));
        itemList.Add(new Item(103, "망가진 태엽 인형", "별 쓸모는 없는 태엽 인형. 속에 이끼가 잔뜩 꼈는지 잘 움직이지 않는다. 어릴 적 가지고 있던 인형과 비슷하게 생겼다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(104, "냄새무당버섯", "붉은색을 띠는 버섯. 먹으면 위험할지도 모르겠다.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
    } 
}