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
    public GameObject creat2; // 파일 삭제 게임시작 선택 UI
    public GameObject creat3; // 파일 삭제완료 메세지 UI

    public TextMeshProUGUI[] slotText; // 슬롯버튼 아래에 존재하는 Text들
    public TextMeshProUGUI[] slotText2; // 슬롯버튼 아래에 존재하는 Text들

    public TMP_Text fileName; // 새로 입력된 파일 이름
    public TMP_InputField inputField; // InputField 컴포넌트를 드래그 앤 드롭으로 연결

    public Image[] slotImages; // 슬롯 버튼 이미지들

    bool[] savefile = new bool[6]; // 세이브파일 존재유무 저장

    public Sprite dataExistsImage; // 이미 데이터가 있는 경우의 이미지
    public Sprite dataEmptyImage; // 데이터가 없는 경우의 이미지

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 6; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}" + "_player.json")) // 데이터가 있는 경우
            {
                savefile[i] = true; // 해당 슬롯 번호의 bool배열 true로 변환
                DataManager.instance.nowSlot = i; // 선택한 슬롯 번호 저장
                DataManager.instance.LoadData(); // 해당 슬롯 데이터 불러옴
                slotText[i].text = DataManager.instance.nowPlayer.filename; // 버튼에 파일이름 표시
                slotText2[i].text = "이어서 하기";
                slotImages[i].sprite = dataExistsImage; // 이미 데이터가 있는 경우의 이미지로 변경
            }
            else // 데이터가 없는 경우
            {
                slotText[i].text = "Empty";
                slotText2[i].text = "새로 하기";
                slotImages[i].sprite = dataEmptyImage; // 데이터가 없는 경우의 이미지로 변경
            }
        }
        // 불러온 데이터를 초기화시킴
        DataManager.instance.DataClear();
        DataManager.instance.InventoryClear();
    }

    public void Slot(int number) // 슬롯의 기능 구현
    {
        InventoryDataManager.Instance.inventoryItemList.Clear();

        Debug.Log("Select - Slot number : " + number);
        Debug.Log("Select - Slot instance nowSlot : " + DataManager.instance.nowSlot);

        DataManager.instance.nowSlot = number; // 슬롯의 번호를 슬롯번호로 입력함

        Debug.Log("Select - Slot number 2222 : " + number);
        Debug.Log("Select - Slot instance nowSlot 2222: " + DataManager.instance.nowSlot);

        if (savefile[number]) // bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            DataManager.instance.LoadData(); // 데이터를 로드하고
            //DataManager.instance.LoadInventory();
            Debug.Log("현재 슬롯에 데이터 YES-----");
            Creat2();
        }
        else // bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
        {
            Debug.Log("현재 슬롯에 데이터 NONE-----");
            Creat();
        }
    }

    public void Creat() // 파일 이름 UI
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
        StartCoroutine(DisplayAndHideCanvas());
        creat2.gameObject.SetActive(false);
    }

    private IEnumerator DisplayAndHideCanvas()
    {
        creat3.gameObject.SetActive(true); 
        yield return new WaitForSeconds(displayDuration); 
        creat3.gameObject.SetActive(false); 
    }

    public void GoGame() // 게임씬으로 이동
    {
        // 현재 슬롯의 데이터가 있는지 확인
        if (savefile[DataManager.instance.nowSlot])
        {
            // 데이터 불러오기
            DataManager.instance.LoadData();
            DataManager.instance.LoadInventory();
        }
        else
        {
            DataManager.instance.FieldDataClear();
            // 새로운 슬롯 정보 저장
            DataManager.instance.nowPlayer.filename = fileName.text;
            DataManager.instance.SaveData(DataManager.instance.nowSlot);

        }
        ChangeScene.target();
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
        Debug.Log("Select -- DeletSlot nowSlot : " + DataManager.instance.nowSlot);

        string filePath = DataManager.instance.path + $"{DataManager.instance.nowSlot}_player.json";
        string filePath2 = DataManager.instance.path + $"{DataManager.instance.nowSlot}_inventory.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            savefile[DataManager.instance.nowSlot] = false;
            slotText[DataManager.instance.nowSlot].text = "Empty";
            slotText2[DataManager.instance.nowSlot].text = "새로 하기";
            slotImages[DataManager.instance.nowSlot].sprite = dataEmptyImage;
        }

        if (File.Exists(filePath2))
        {
            File.Delete(filePath2);
        }
        
        Debug.Log("Select -- DeletSlot nowSlot222222222 : " + DataManager.instance.nowSlot);
    }
}