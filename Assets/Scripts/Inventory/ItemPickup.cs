using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
// 아이템 습득을 구현한 스크립트. 맵에 배치되는 오브젝트에 들어갈 스크립트임.

=======
>>>>>>> 1f78285fb3a123f08c9a267a909af1f923f16e51
public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    private float interactionDistance = 2.0f; // 상호작용 가능 최대 거리

    public int itemID;
    public int _count;

    private void OnTriggerStay(Collider collision)
    {
        float distance = Vector3.Distance(collision.transform.position, this.gameObject.transform.position); // 콜라이더와 오브젝트의 거리 계산

        if (distance < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
                    {
                        Inventory.instance.GetAnItem(itemID, _count);

                        Destroy(this.gameObject);
                    }
        }
    }
}