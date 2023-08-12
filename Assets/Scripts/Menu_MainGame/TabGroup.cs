using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons; // 탭 그룹에 속한 모든 탭 버튼들의 리스트
    private int selectedTabIndex = 0; // 선택된 탭 버튼의 인덱스

    public int GetTabButtonIndex(TabButton button)
    {
        return tabButtons.IndexOf(button);
    }

    public void OnTabSelected(TabButton button)
    {
        int buttonIndex = tabButtons.IndexOf(button);
        if (buttonIndex != -1)
        {
            selectedTabIndex = buttonIndex; // 선택된 탭 버튼의 인덱스를 업데이트
            UpdateTabButtonColors(); // 탭 버튼의 색상을 업데이트
        }
    }

    private void UpdateTabButtonColors()
    {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (i == selectedTabIndex)
            {
                tabButtons[i].SetTextColor(Color.white); // 선택된 탭 버튼의 텍스트 색상을 하얀색으로 설정
            }
            else
            {
                tabButtons[i].SetTextColor(Color.gray); // 선택되지 않은 탭 버튼의 텍스트 색상을 회색으로 설정
            }
        }
    }
}
