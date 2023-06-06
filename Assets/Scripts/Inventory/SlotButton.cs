using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
// 슬롯 클릭을 구현한 스크립트.

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
public class SlotButton : MonoBehaviour
{
    public int slotIndex;

    public void OnButtonClick()
    {
        Inventory.instance.selectedSlot = slotIndex;
    }
}
