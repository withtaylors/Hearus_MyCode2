using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
// 아이템 클래스를 구현한 스크립트.

[System.Serializable]
public class Item
{
=======
[System.Serializable]
public class Item
{
    // 아이템 클래스

>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    public int itemID; // 아이템 고유 ID 값
    public string itemName; // 아이템 이름
    public string itemDescription; // 아이템 설명
    public int itemCount; // 소지 개수
<<<<<<< HEAD
    public int itemEffectValue;
    public Sprite itemIcon;
    public ItemType itemType;
    public ItemEffect itemEffect;
=======
    public Sprite itemIcon;
    public ItemType itemType;
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51

    public enum ItemType
    {
        소모품,
        도구,
        재료,
        부품,
        연료
    }

<<<<<<< HEAD
    public enum ItemEffect
    {
        회복,
        피해,
        기타
    }

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, ItemEffect _itemEffect, int _itemEffectValue, int _itemCount = 1)   // 생성자
=======
    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)   // 생성자
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    {
        // itemID는 아이콘 파일의 이름과 같게 함
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
<<<<<<< HEAD
        itemEffect = _itemEffect;
        itemEffectValue = _itemEffectValue;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    public Item CloneItem(Item _item)
    {
        Item cloneItem = new Item(_item.itemID, _item.itemName, _item.itemDescription, _item.itemType, _item.itemEffect, _item.itemEffectValue, 1);

        return cloneItem;
=======
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

    public void CloneItem(Item _item)
    {
        
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    }
}
