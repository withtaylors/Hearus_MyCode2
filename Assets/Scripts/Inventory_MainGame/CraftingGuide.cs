using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingGuide : MonoBehaviour
{
    public Transform tf_materialSlots;

    public GameObject GuidePanel;
    public GameObject DescriptionPanel;
    public GameObject CraftingPanel;
    public GameObject Slots;
    public GameObject ExitButton;
    public GameObject BackButton;

    private void OnEnable()
    {
        for (int i = 0; i < tf_materialSlots.childCount; i++)
        {
            Transform materialSlot = tf_materialSlots.GetChild(i);
            
            for (int j = 0; i < ItemDatabase.itemList.Count; i++)
            {
                if (materialSlot.gameObject.name == ItemDatabase.itemList[j].itemName)
                {
                    if (ItemDatabase.itemList[j].isMeet == true) // 획득한 적 있는 아이템이라면
                    {
                        materialSlot.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemDatabase.itemList[j].itemName; // 가이드 슬롯의 텍스트를 itemName으로 바꿈

                        Color color = materialSlot.GetChild(1).GetComponent<Image>().color; // 가이드 슬롯의 아이콘 알파 값을 조정함
                        color.a = 255f;
                        materialSlot.GetChild(1).GetComponent<Image>().color = color;
                    }
                }
            }
        }
    }

    public void OnGuideButtonClick()
    {
        DescriptionPanel.SetActive(false);
        CraftingPanel.SetActive(false);
        Slots.SetActive(false);
        ExitButton.SetActive(false);

        GuidePanel.SetActive(true);
        BackButton.SetActive(true);
    }

    public void OnBackButtonClick()
    {
        DescriptionPanel.SetActive(true);
        CraftingPanel.SetActive(true);
        Slots.SetActive(true);
        ExitButton.SetActive(true);

        GuidePanel.SetActive(false);
    }
}
