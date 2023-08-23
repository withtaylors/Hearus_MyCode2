using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete : MonoBehaviour
{
    public Button[] buttons;

    private Select selectScript; // Select 스크립트의 인스턴스를 저장할 변수

    private void Start()
    {
        selectScript = FindObjectOfType<Select>(); // Select 스크립트의 인스턴스를 가져옴

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }
    
    private void OnButtonClick(int buttonIndex)
    {
        Debug.Log($"Button {buttonIndex} clicked!");
        selectScript.DeleteSelectedSlot(); // Select 스크립트의 DeleteSelectedSlot 메서드 호출
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectScript.DeleteSelectedSlot(); // 오른쪽 마우스 클릭 시 슬롯 삭제 기능 실행
            Debug.Log("오른쪽마우스인식");
        }
    }
}
