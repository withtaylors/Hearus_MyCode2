using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class Save : MonoBehaviour
{
    public GameObject creat; // 파일 이름 입력UI
    public GameObject creat2; // 파일 삭제 게임시작 선택 UI
    public GameObject creat3; // 파일 삭제완료 메세지 UI
    public GameObject creat4; // 파일 저장완료 메세지 UI

    public TextMeshProUGUI[] slotText; // 슬롯버튼 아래에 존재하는 Text들
    public TextMeshProUGUI[] slotText2; // 슬롯버튼 아래에 존재하는 Text들

    public TMP_Text fileName; // 새로 입력된 파일 이름
    public TMP_InputField inputField; // InputField 컴포넌트를 드래그 앤 드롭으로 연결

    public Image[] slotImages; // 슬롯 버튼 이미지들

    bool[] savefile = new bool[6]; // 세이브파일 존재유무 저장
    int selectedSlot; // 추가: 선택한 슬롯 번호 저장

    public Sprite dataExistsImage; // 이미 데이터가 있는 경우의 이미지
    public Sprite dataEmptyImage; // 데이터가 없는 경우의 이미지

    private void Awake()
    {
        // DataManager 스크립트를 참조하고 있는 게임 오브젝트로부터 DataManager 클래스의 instance를 얻습니다.
        DataManager.instance = FindObjectOfType<DataManager>();

        // 초기 데이터 설정
        DataManager.instance.DataClear();
    }

    void Start()
    {
        StartCoroutine(UpdateSlotTextsWithDelay());
    }

    private IEnumerator UpdateSlotTextsWithDelay()
    {
        while (true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (File.Exists(DataManager.instance.path + $"{i}_player.json")) // 데이터가 있는 경우
                {
                    savefile[i] = true; // 해당 슬롯 번호의 bool배열 true로 변환
                    DataManager.instance.nowSlot = i; // 선택한 슬롯 번호 저장
                    DataManager.instance.LoadData(); // 해당 슬롯 데이터 불러옴
                    slotText[i].text = DataManager.instance.nowPlayer.filename; // 버튼에 파일이름 표시
                    slotText2[i].text = "이어서 저장";
                    slotImages[i].sprite = dataExistsImage; // 이미 데이터가 있는 경우의 이미지로 변경
                }
                else // 데이터가 없는 경우
                {
                    slotText[i].text = "Empty";
                    slotText2[i].text = "새로 저장";
                    slotImages[i].sprite = dataEmptyImage; // 데이터가 없는 경우의 이미지로 변경
                }
            }
            
            // 무조건 1초 간격으로 대기
            float startTime = Time.realtimeSinceStartup;
            float waitDuration = 1.0f;

            while (Time.realtimeSinceStartup - startTime < waitDuration)
            {
                yield return null; // 다음 프레임까지 대기
            }
        }
    }

    public void Slot(int number)
    {
        selectedSlot = number; // 선택한 슬롯 번호 저장

        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            Creat2();
            //DataManager.instance.SaveData(selectedSlot); // 선택한 슬롯에 데이터 저장하도록 수정
        }
        else
        {
            if (!savefile[DataManager.instance.nowSlot])
            {
                // 파일 이름 입력 UI를 활성화
                creat.gameObject.SetActive(true);

                // 이어서 저장 기능 구현
                if (savefile[number])
                {
                    DataManager.instance.nowSlot = selectedSlot; // 선택한 슬롯으로 변경
                    DataManager.instance.LoadData();
                }
            }
        }
    }

    public void Creat2() // 파일 삭제 or 게임시작 선택 UI
    {
        creat2.gameObject.SetActive(true);
    }

    public void Creat3() // 파일 삭제완료 메세지 UI
    {
        StartCoroutine(DisplayAndHideDeleteCanvas());
        creat2.gameObject.SetActive(false);
    }

    public void Creat4() // 파일 저장완료 메세지 UI
    {
        StartCoroutine(DisplayAndHideSaveCanvas());
        creat2.gameObject.SetActive(false);
    }

    private IEnumerator DisplayAndHideDeleteCanvas()
    {
        creat3.gameObject.SetActive(true);
        
        float startTime = Time.realtimeSinceStartup; // 현재 실제 시간 저장
        float waitDuration = 2.0f; // 대기할 시간

        while (Time.realtimeSinceStartup - startTime < waitDuration)
        {
            yield return null; // 다음 프레임까지 대기
        }

        creat3.gameObject.SetActive(false);
    }

    private IEnumerator DisplayAndHideSaveCanvas()
    {
        creat4.gameObject.SetActive(true);
        
        float startTime = Time.realtimeSinceStartup; // 현재 실제 시간 저장
        float waitDuration = 2.0f; // 대기할 시간

        while (Time.realtimeSinceStartup - startTime < waitDuration)
        {
            yield return null; // 다음 프레임까지 대기
        }

        creat4.gameObject.SetActive(false);
    }

    public void SaveAgain()
    {
        Debug.Log("현재선택한 슬롯 : " + selectedSlot);
        DataManager.instance.SaveData(selectedSlot); // 선택한 슬롯에 데이터 저장하도록 수정
    }

    public void SaveFileAgain()
    {
        for (int i = 0; i < 6; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}_player.json"))
            {
                DataManager.instance.SaveData(i); // 파라미터 추가
            }
        }

        if (File.Exists(DataManager.instance.path + $"{DataManager.instance.nowSlot}_player.json"))
        {
            DataManager.instance.SaveData(DataManager.instance.nowSlot); // 파라미터 추가
        }
    }

    public void Cancel() 
    {
        creat.gameObject.SetActive(false);
        if (inputField != null)
        {
            inputField.text = ""; // 입력된 텍스트를 비워줍니다.
        }    
    }

    public void DeleteSlot()
    {
        string filePath = DataManager.instance.path + $"{DataManager.instance.nowSlot}_player.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            // savefile[DataManager.instance.nowSlot] = false;
            // slotText[DataManager.instance.nowSlot].text = "Empty";
            // slotText2[DataManager.instance.nowSlot].text = "새로 하기";
            // slotImages[DataManager.instance.nowSlot].sprite = dataEmptyImage;
        }
    }
}
