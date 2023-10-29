using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PresentToFrith : MonoBehaviour
{
    public static PresentToFrith instance;

    public Transform recievePanel;
    public Transform refusePanel;

    private Item currentItem;

    private void Awake()
    {
        instance = this;
    }

    public void OnPresentButtonClick() // 인벤토리에서 선물 버튼을 눌렀을 때
    {
        if (DataManager.instance.nowPlayer.nowCharacter == "None")
            return;

        currentItem = Inventory.instance.inventoryItemList[Inventory.instance.selectedSlot];

        if (currentItem.present)
        {
            // 선물이 가능하다면
            // 프리스가 선물을 받았다는 메시지 출력
            // 인벤토리에서 해당 아이템을 1개 삭제
            recievePanel.gameObject.SetActive(true);
            Inventory.instance.DeleteItem(currentItem.itemID);
            StartCoroutine(FadeOutPanel(recievePanel));
        }
        else
        {
            // 선물이 불가능하다면
            // 프리스는 선물을 받지 않았다는 메시지 출력
            refusePanel.gameObject.SetActive(true);
            StartCoroutine(FadeOutPanel(refusePanel));
        }
    }

    public void OnPresentChoice(string _string) // 프리스에게 준다는 선택지를 골랐을 때
    {
        if (_string == "N")
        {
            // 선물이 불가능하다면
            // 프리스는 선물을 받지 않았다는 메시지 출력
            refusePanel.gameObject.SetActive(true);
            StartCoroutine(FadeOutPanel(recievePanel));
        }
        else
        {
            // 선물이 가능하다면
            // 프리스가 선물을 받았다는 메시지 출력
            recievePanel.gameObject.SetActive(true);
            StartCoroutine(FadeOutPanel(recievePanel));
        }
    }

    private IEnumerator FadeOutPanel(Transform transform)
    {
        transform.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2.5f);

        Color c = transform.GetComponent<Image>().color;

        for (float f = 1f; f >= 0f; f -= 0.25f)
        {
            c.a = f;
            transform.GetComponent<Image>().color = c;
            yield return null;
        }

        transform.gameObject.SetActive(false);
    }
}
