using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

<<<<<<< HEAD
// 인벤토리 슬롯에 대한 기능을 구현한 스크립트. 

public class InventorySlot : MonoBehaviour
{
    public static InventorySlot instance;

    public Image icon;
    public TextMeshProUGUI itemCount_Text;

    private void Awake()
    {
        instance = this;
    }

=======
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemCount_Text;

>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    public void Additem(Item _item) {
        icon.sprite = _item.itemIcon;           // 슬롯 아이콘 변경
        Color color = icon.color;
        color.a = 255f;
        icon.color = color;                     // 슬롯 이미지의 알파 값 조정
<<<<<<< HEAD
        if (_item.itemCount > 0)
        {
            itemCount_Text.text = _item.itemCount.ToString();
        }
        else
            itemCount_Text.text = "";
    }

    public void IncreaseCount(Item _item)
=======
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
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
    {
            if (_item.itemCount > 0)
            {
                _item.itemCount += 1;
                itemCount_Text.text = _item.itemCount.ToString();
            }
            else
                itemCount_Text.text = "";
    }

<<<<<<< HEAD
    public void DecreaseCount(Item _item)
    {
        if (_item.itemCount > 1)
        {
            _item.itemCount -= 1;
            itemCount_Text.text = _item.itemCount.ToString();
        }
        else
            Debug.Log("유효하지 않은 명령입니다.");
    }

    public void RemoveItem()
    {
        Color color = icon.color;
        color.a = 0f;
        icon.color = color;                     // 슬롯 알파 값 0으로 조정
        itemCount_Text.text = "";               // count를 나타내는 텍스트 null로 조정
        icon.sprite = null;                     // 슬롯 이미지 null로 조정
    }
}
=======
    public void RemoveItem()
    {
        itemCount_Text.text = "";
        icon.sprite = null;
    }
}
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
