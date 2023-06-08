using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class TabButton : MonoBehaviour
{
    [SerializeField] private Image background; // 탭 버튼의 배경 이미지
    [SerializeField] private Sprite idleImg; // 선택되지 않은 상태의 이미지
    [SerializeField] private Sprite selectedImg; // 선택된 상태의 이미지
    [SerializeField] private TextMeshProUGUI text; // 탭 버튼의 텍스트

    private TabGroup tabGroup; // 해당 탭 버튼이 속한 탭 그룹


    public void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        tabGroup = GetComponentInParent<TabGroup>();

        // 첫 번째 탭 버튼일 경우 텍스트 색상을 하얀색으로 설정
        if (tabGroup != null && tabGroup.GetTabButtonIndex(this) == 0)
        {
            text.color = Color.white;
        }
    }

    public void Selected()
    {
        background.sprite = selectedImg;
        if (text != null)
        {
            SetTextColor(Color.white); // 텍스트 색상을 하얀색으로 설정
        }
        else
        {
            Debug.LogError("Text component is not assigned!");
        }
    }

    public void DeSelected()
    {
        background.sprite = idleImg;
        SetTextColor(Color.gray); // 텍스트 색상을 회색으로 설정
    }

    public void OnButtonClick()
    {
        if (tabGroup != null)
        {
            tabGroup.OnTabSelected(this); // 해당 탭 버튼이 클릭되었을 때 탭 그룹에 알림을 보냄
        }
        else
        {
            Debug.LogError("TabGroup not found!");
        }
    }

    public void SetTextColor(Color color)
    {
        if (text != null)
        {
            text.color = color;
        }
    }
}