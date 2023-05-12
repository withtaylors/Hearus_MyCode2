using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    // 아이템 클래스

    public int itemID; // 아이템 고유 ID 값
    public string itemName; // 아이템 이름
    public string itemDescription; // 아이템 설명
    public int itemCount; // 소지 개수
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        소모품,
        장비,
        부품,
        연료
    }

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)   // 생성자
    {
        // itemID는 아이콘 파일의 이름과 같게 함
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}
