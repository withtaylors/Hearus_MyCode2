using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    public int _itemID;
    public int _count;

    public void Pickup(GameObject item)
    {
        int pickingID;
        int pickingCount;

        pickingID = item.gameObject.GetComponent<ItemPickup>()._itemID;
        pickingCount = item.gameObject.GetComponent<ItemPickup>()._count;

        Inventory.instance.GetAnItem(pickingID, pickingCount);
    }
}