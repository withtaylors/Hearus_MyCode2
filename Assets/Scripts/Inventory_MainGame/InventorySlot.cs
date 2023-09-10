using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
    public static InventorySlot instance;

    public Image icon;
    public Button slotButton;
    public TextMeshProUGUI itemCount_Text;

    public int slotIndex;

    private void Awake()
    {
        instance = this;
    }

    public void Additem(Item _item) {
        icon.sprite = _item.itemIcon;           // 슬롯 아이콘 변경
        Color color = icon.color;
        color.a = 255f;
        icon.color = color;                     // 슬롯 이미지의 알파 값 조정

        if (_item.itemCount > 0)
        {
            itemCount_Text.text = _item.itemCount.ToString();
        }
        else
            itemCount_Text.text = "";
    }

    public void UpdateItemCount(Item _item)
    {
        if (_item.itemCount > 0)
        {
            itemCount_Text.text = _item.itemCount.ToString();
        }
        else
            itemCount_Text.text = string.Empty;
    }

    public void UncountableItem(Item _item)
    {
        if (_item.isCountable == false)
            itemCount_Text.text = string.Empty;
    }

    public void IncreaseCount(Item _item)
    {
            if (_item.itemCount > 0)
            {
                _item.itemCount += 1;
                itemCount_Text.text = _item.itemCount.ToString();
            }
            else
                itemCount_Text.text = "";
    }

    public void DecreaseCount(Item _item)
    {
        if (_item.itemCount > 1)
        {
            _item.itemCount -= 1;
            itemCount_Text.text = _item.itemCount.ToString();
        }
        else
            return;
    }

    public void RemoveItem()
    {
        itemCount_Text.text = "";
        icon.sprite = null;
        Color color = icon.color;
        color.a = 0f;
        icon.color = color;
    }

    public void OnButtonClick()
    {
        Inventory.instance.selectedSlot = slotIndex;
    }
}
