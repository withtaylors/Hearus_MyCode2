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
        itemList.Add(new Item(101, "넝쿨", "밧줄을 만들 수 있는 꽤 질긴 넝쿨.", Item.ItemType.재료, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(102, "밧줄", "넝쿨로 만든 꽤 튼튼한 밧줄. 여러 번 쓸 수 있다.", Item.ItemType.소모품, Item.ItemEffect.기타, 0, 10));
        itemList.Add(new Item(103, "망가진 태엽 인형", "별 쓸모는 없는 태엽 인형. 속에 이끼가 잔뜩 꼈는지 잘 움직이지 않는다. 어릴 적 가지고 있던 인형과 비슷하게 생겼다.", Item.ItemType.기타, Item.ItemEffect.기타, 0));
        itemList.Add(new Item(104, "냄새무당버섯", "선명한 빨간색의 독버섯.", Item.ItemType.소모품, Item.ItemEffect.피해, 10));
        itemList.Add(new Item(105, "버터넛", "부드럽고 달콤한 호박. 크기가 커 배부르게 먹을 수 있다.", Item.ItemType.소모품, Item.ItemEffect.회복, 10));
    } 
}