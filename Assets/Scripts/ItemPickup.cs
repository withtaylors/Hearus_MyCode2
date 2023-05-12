using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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