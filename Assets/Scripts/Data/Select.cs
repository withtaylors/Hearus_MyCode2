using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat; // 파일 이름 입력UI
    public TextMeshProUGUI[] slotText; // 슬롯버튼 아래에 존재하는 Text들
    public TextMeshProUGUI[] slotText2; // 슬롯버튼 아래에 존재하는 Text들
    public TMP_Text fileName; // 새로 입력된 파일 이름
    public Image[] slotImages; // 슬롯 버튼 이미지들

    bool[] savefile = new bool[6]; // 세이브파일 존재유무 저장

    public Sprite dataExistsImage; // 이미 데이터가 있는 경우의 이미지
    public Sprite dataEmptyImage; // 데이터가 없는 경우의 이미지

    public static Select instance; // 싱글톤 인스턴스 추가

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        DataManager.instance.OnSelectedSlotChanged += UpdateSelectedSlotUI;
        UpdateSlotTexts();
    }

    private void UpdateSlotTexts()
    {
        for (int i = 0; i < 6; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}_player.json"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.filename;
                slotText2[i].text = "이어서 하기";
                slotImages[i].sprite = dataExistsImage;
            }
            else
            {
                slotText[i].text = "Empty";
                slotText2[i].text = "새로 하기";
                slotImages[i].sprite = dataEmptyImage;
            }
        }
        DataManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        DataManager.instance.SetSelectedSlot(number);
        if (savefile[number])
        {
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            Creat();
        }
    }

    private void UpdateSelectedSlotUI(int selectedSlot)
    {
        // 선택된 슬롯에 대한 UI 업데이트
        if (selectedSlot >= 0 && selectedSlot < savefile.Length)
        {
            // 해당 슬롯의 UI를 업데이트
            slotText[selectedSlot].text = DataManager.instance.nowPlayer.filename;
            slotText2[selectedSlot].text = "이어서 하기";
            slotImages[selectedSlot].sprite = dataExistsImage;
        }
    }

    public void Creat() // 파일 이름 입력 UI를 활성화하는 메소드
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame() // 게임씬으로 이동
    {
        if (!savefile[DataManager.instance.nowSlot]) // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.instance.nowPlayer.filename = fileName.text; // 입력한 이름을 복사해옴
            DataManager.instance.SaveData(DataManager.instance.nowSlot); // 현재 슬롯 정보를 저장함.
        }
        ChangeScene.target(); // ChangeScene에 대한 처리 코드가 빠져있어서 별도로 구현해야 합니다.
    }

    public void Cancel() 
    {
        creat.gameObject.SetActive(false);
    }

    public void DeleteSelectedSlot() // 이 함수에서 오른쪽 마우스 클릭으로 슬롯 삭제 기능을 구현
    {
        int selectedSlot = DataManager.instance.nowSlot; // 현재 선택된 슬롯 번호를 가져옴

        if (selectedSlot >= 0 && selectedSlot < savefile.Length)
        {
            if (File.Exists(DataManager.instance.path + $"{selectedSlot}_player.json"))
            {
                File.Delete(DataManager.instance.path + $"{selectedSlot}_player.json");

                // savefile 배열 업데이트
                savefile[selectedSlot] = false;

                // 해당 슬롯의 UI 업데이트
                slotText[selectedSlot].text = "Empty";
                slotText2[selectedSlot].text = "새로 저장";
                slotImages[selectedSlot].sprite = dataEmptyImage;

                Debug.Log($"Slot {selectedSlot}의 파일이 삭제되었습니다.");
            }
        }
    }
}