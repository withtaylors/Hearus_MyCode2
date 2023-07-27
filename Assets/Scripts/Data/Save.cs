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
    public TextMeshProUGUI[] slotText; // 슬롯버튼 아래에 존재하는 Text들
    public TextMeshProUGUI[] slotText2; // 슬롯버튼 아래에 존재하는 Text들
    public TMP_Text fileName; // 새로 입력된 파일 이름
    public Image[] slotImages; // 슬롯 버튼 이미지들

    bool[] savefile = new bool[3]; // 세이브파일 존재유무 저장

    public Sprite dataExistsImage; // 이미 데이터가 있 는 경우의 이미지
    public Sprite dataEmptyImage; // 데이터가 없는 경우의 이미지

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}")) // 데이터가 있는 경우
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
        // 불러온 데이터를 초기화시킴
        DataManager.instance.DataClear();
    }

    public void Slot(int number) // 슬롯의 기능 구현
    {
        DataManager.instance.nowSlot = number; // 슬롯의 번호를 슬롯번호로 입력함.

        if (savefile[number]) // bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            DataManager.instance.LoadData(); // 데이터를 로드하고
        }
        else // bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
        {
            //creat.SetActive(true); ; // 파일 이름 입력 UI 활성화
            if (!savefile[DataManager.instance.nowSlot]) // 현재 슬롯번호의 데이터가 없다면
            {
                DataManager.instance.nowPlayer.filename = fileName.text; // 입력한 이름을 복사해옴
                Debug.Log("이름입력은 인식함");
                DataManager.instance.SaveData(); // 현재 정보를 저장함

                if (savefile[number]) // bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
                {
                    DataManager.instance.LoadData(); // 데이터를 로드하고
                }
            }
        }
    }
}