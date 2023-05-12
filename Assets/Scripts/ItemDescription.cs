using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName_Text;
    public TextMeshProUGUI itemType_Text;
    public TextMeshProUGUI itemDes_Text;

    public void DisplayDes(Item _item)
    {
        icon.sprite = _item.itemIcon;
        itemName_Text.text = _item.itemName;
        itemType_Text.text = _item.itemType.ToString();
        itemDes_Text.text = _item.itemDescription;
    }
}
