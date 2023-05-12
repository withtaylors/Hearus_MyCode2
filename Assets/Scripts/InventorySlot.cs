using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemCount_Text;

    public void Additem(Item _item) {
        icon.sprite = _item.itemIcon;           // 슬롯 아이콘 변경
        Color color = icon.color;
        color.a = 255f;
        icon.color = color;                     // 슬롯 이미지의 알파 값 조정
        if(Item.ItemType.소모품 == _item.itemType) // 아이템이 소모품일 경우 수량 표시
        {
            if (_item.itemCount > 0)
            {
                itemCount_Text.text = _item.itemCount.ToString();
            }
            else
                itemCount_Text.text = "";
        }
    }

    public void setItemCount(Item _item)
    {
        if(Item.ItemType.소모품 == _item.itemType)
        {
            if (_item.itemCount > 0)
            {
                _item.itemCount += 1;
                itemCount_Text.text = _item.itemCount.ToString();
            }
            else
                itemCount_Text.text = "";
        }
    }

    public void RemoveItem()
    {
        itemCount_Text.text = "";
        icon.sprite = null;
    }
}
