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

    IEnumerator UpdateSlotTextsWithDelay()
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
            
            yield return new WaitForSeconds(1f); // 1초마다 슬롯 정보 업데이트
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
                Creat();

                // 이어서 저장 기능 구현
                if (savefile[number])
                {
                    DataManager.instance.nowSlot = selectedSlot; // 선택한 슬롯으로 변경
                    DataManager.instance.LoadData();
                }
            }
        }
    }

    public void Creat() // 파일 이름 입력 UI를 활성화하는 메소드
    {
        creat.gameObject.SetActive(true);
    }

    public float displayDuration = 2.0f;

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
        creat3.gameObject.SetActive(true); // Show creat3 canvas
        yield return new WaitForSeconds(displayDuration); // Wait for the display duration
        creat3.gameObject.SetActive(false); // Hide creat3 canvas after the display duration
    }

    private IEnumerator DisplayAndHideSaveCanvas()
    {
        creat4.gameObject.SetActive(true); // Show creat3 canvas
        yield return new WaitForSeconds(displayDuration); // Wait for the display duration
        creat4.gameObject.SetActive(false); // Hide creat3 canvas after the display duration
    }

    public void OnOKButtonClick()
    {
        if (fileName.text != "")
        {
            DataManager.instance.nowPlayer.filename = fileName.text;
            Debug.Log(fileName.text + ": 파일 이름임");
            DataManager.instance.SaveData(selectedSlot); // 선택한 슬롯에 데이터 저장하도록 수정

            int slotNumber = DataManager.instance.nowSlot;
            savefile[slotNumber] = true; // 해당 슬롯 번호의 bool배열 true로 변환
            slotText[slotNumber].text = DataManager.instance.nowPlayer.filename; // 버튼에 파일이름 표시
            slotText2[slotNumber].text = "이어서 저장";
            slotImages[slotNumber].sprite = dataExistsImage; // 이미 데이터가 있는 경우의 이미지로 변경

            creat.gameObject.SetActive(false);
        }
    }

    public void SaveAgain()
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
    }

    public void SaveFileAgain()
    {
        DataManager.instance.SaveData(selectedSlot); // 선택한 슬롯에 데이터 저장하도록 수정
    }

    public void DeleteSlot()
    {
        string filePath = DataManager.instance.path + $"{DataManager.instance.nowSlot}_player.json";

        if (File.Exists(filePath))
        {
            Debug.Log("파일 존재함");
            File.Delete(filePath);
            savefile[DataManager.instance.nowSlot] = false;
            slotText[DataManager.instance.nowSlot].text = "Empty";
            slotText2[DataManager.instance.nowSlot].text = "새로 하기";
            slotImages[DataManager.instance.nowSlot].sprite = dataEmptyImage;
        }
    }
}
