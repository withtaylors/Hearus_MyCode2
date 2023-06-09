using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabPanel : MonoBehaviour
{
    public List<TabButton> tabButtons; // 탭 패널에 속한 모든 탭 버튼들의 리스트
    public List<GameObject> contentsPanels; // 탭 패널에 속한 모든 컨텐츠 패널들의 리스트

    private int selectedTabIndex = -1;    // 선택된 탭 버튼의 인덱스 (-1은 초기 상태)

    public void Start()
    {
        ActivateFirstTab(); // 시작할 때 첫 번째 탭을 활성화
    }

    public void ClickTab(int tabIndex)
    {
        if (selectedTabIndex == tabIndex)
            return; // 이미 선택된 탭을 클릭한 경우 아무 작업도 수행하지 않고 종료

        for (int i = 0; i < contentsPanels.Count; i++)
        {
            if (i == tabIndex)
            {
                contentsPanels[i].SetActive(true); // 선택된 탭 버튼에 해당하는 컨텐츠 패널을 활성화
                tabButtons[i].Selected(); // 선택된 탭 버튼의 외관을 변경
            }
            else
            {
                contentsPanels[i].SetActive(false); // 선택되지 않은 탭 버튼에 해당하는 컨텐츠 패널을 비활성화
                tabButtons[i].DeSelected(); // 선택되지 않은 탭 버튼의 외관을 변경
            }
        }

        selectedTabIndex = tabIndex;
    }

    public void ActivateFirstTab()
    {
        if (tabButtons.Count > 0)
        {
            selectedTabIndex = -1; // 초기 상태로 설정
            ClickTab(0); // 첫 번째 탭을 클릭하여 활성화
        }
    }
}
