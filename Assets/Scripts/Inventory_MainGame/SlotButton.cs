using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotButton : MonoBehaviour
{
    public int slotIndex;

    public void OnButtonClick()
    {
        Inventory.instance.selectedSlot = slotIndex;
    }
}
