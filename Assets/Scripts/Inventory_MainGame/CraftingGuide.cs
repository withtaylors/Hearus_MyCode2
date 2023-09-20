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
        for (int i = 0; i < tf_materialSlots.childCount; i++) // 재료 슬롯 수만큼 반복
        {
            Transform materialSlot = tf_materialSlots.GetChild(i); // i번째 슬롯

            // getItemIDList(획득 이력 있는 아이템의 아이디 리스트) 검색
            for (int j = 0; j < DataManager.instance.dataWrapper.getItemIDList.Count; j++)
            {
                // 획득한 적 있는 아이템이라면
                if (materialSlot.gameObject.name == DataManager.instance.dataWrapper.getItemIDList[j].ToString())
                {
                    // 아이템 데이터베이스 검색
                    for (int k = 0; k < ItemDatabase.itemList.Count; k++)
                    {
                        // 아이템 이름 가져오기
                        if (materialSlot.gameObject.name == ItemDatabase.itemList[k].itemID.ToString())
                        {
                            // 재료 슬롯의 텍스트를 해당 아이템의 이름으로 바꾸기
                            materialSlot.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemDatabase.itemList[k].itemName;

                            Color color = materialSlot.GetChild(1).GetComponent<Image>().color; // 가이드 슬롯의 아이콘 알파 값을 조정함
                            color.a = 255f;
                            materialSlot.GetChild(1).GetComponent<Image>().color = color;
                        }
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
