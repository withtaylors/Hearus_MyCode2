using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.ParticleSystem;
using Yarn.Unity;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    /// <summary>
    /// 수정중!!!!!!!
    /// </summary>

    [SerializeField] private bool NEXT_STEP_POSSIBLE; // 다음 단계로 넘어갈 수 있는지
    [SerializeField] private int tutorialStep; // 현재 단계

    private List<string> textList = new List<string> { "방향 키를 눌러 이동할 수 있습니다.", // 0
                                                       "스페이스바 키를 눌러 점프할 수 있습니다.", // 1
                                                       "E 키를 눌러 아이템을 조사할 수 있습니다.", // 2
                                                       "습득한 아이템은 인벤토리 창에서 확인할 수 있습니다.", // 3
                                                       "이제 넝쿨을 세 개 모아 봅시다.", // 4
                                                       "습득한 아이템으로 도구를 만들 수 있습니다.\n인벤토리를 열어 보세요.", // 5
                                                       "모은 넝쿨을 선택하여 조합할 수 있습니다.\n선택 버튼을 눌러 주세요.", // 6
                                                       "조합 버튼을 눌러 주세요.", // 7
                                                       "만들어진 도구는 인벤토리에서 확인할 수 있습니다.\n도구를 만든 재료는 사라집니다.", // 8
                                                       "아까 만든 밧줄을 사용해 건너가 봅시다.", // 9
                                                       "인벤토리 창에서 아이템을 선택하면 사용할 수 있습니다.\n밧줄은 C 키를 눌러 타고 올라갈 수 있습니다.\n다시 C 키를 누르면 밧줄에서 내려올 수 있습니다.", // 10
                                                       "튜토리얼이 모두 끝났습니다.\n함께할 파트너를 선택해 주세요." }; // 11


    public GameObject tutorialPanel; // 튜토리얼 문구 패널
    public TextMeshProUGUI tutorialPanelText; // 튜토리얼 문구 텍스트

    [SerializeField] private GameObject frithCheckPanel; // 프리스 선택 체크 패널
    [SerializeField] private TextMeshProUGUI frithCheckText; // 프리스 선택 체크 텍스트

    public GameObject Button; 
    [SerializeField] private GameObject nextButton; // 다음 버튼

    [SerializeField] private GameObject settingPanel; // 설정 패널
    [SerializeField] private GameObject selectFrithPanel; // 프리스 선택 패널
    [SerializeField] private ParticleSystem settingButtonParticle; // 설정 버튼 파티클
    [SerializeField] private ParticleSystem inventoryButtonParticle; // 인벤토리 버튼 파티클
    [SerializeField] private GameObject fader; // 페이드 아웃을 위한 오브젝트
    
    [SerializeField] private ScriptManager scriptManager; // 스크립트 매니저


    private bool right = false; // 상하좌우 이동
    private bool left = false; // 상하좌우 이동
    private bool jump = false; // 상하좌우 이동
    [SerializeField] private bool useRope = false; // 로프 사용
    [SerializeField] private bool arriveRopeField = false; // 밧줄 사용 지점 도달하면 true

    public GameObject edenGameObject;
    public GameObject noahGameObject;
    public GameObject adamGameObject;
    public GameObject jonahGameObject;

    private string selectedCharacter;
    private string nowCharacter;

    private void Start()
    {
        if (DataManager.instance.nowPlayer.isFinishedTutorial)
            gameObject.SetActive(false);
        else
            StartTutorial();
    }

    private void Update() // 다음 스텝으로 넘어가기 위한 조건 확인
    {
        switch (tutorialStep)
        {
            case 0: // 조건: 좌우 방향 키 입력(FirstStep)
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    right = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    left = true;
                if (right && left)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 1: // 조건: 스페이스바 키 입력(SecondStep)
                if (Input.GetKeyDown(KeyCode.Space))
                    jump = true;
                if (jump)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 2: // 조건: 인벤토리에 무언가를 습득(ThirdStep)
                if (Inventory.instance.inventoryItemList.Count != 0)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 3: // 조건: 인벤토리 활성화(FourthStep)
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 4: // 조건: 넝쿨 3개 습득(FifthStep)
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 101 && Inventory.instance.inventoryItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 5: // 조건: 인벤토리 활성화(SixthStep)
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 6: // 조건: 크래프팅 아이템 리스트에 넝쿨 3개(SeventhStep)
                for (int i = 0; i < Crafting.instance.craftingItemList.Count; i++)
                    if (Crafting.instance.craftingItemList[i].itemID == 101 && Crafting.instance.craftingItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 7: // 조건: 밧줄 제작(EighthStep)
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 102)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 8:
                break;
            case 9: // 조건: 밧줄 사용 지점에 도달하기(NinthStep)
                if (arriveRopeField)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton2();
                break;
            case 10: // 조건: 인벤토리 활성화(TenthStep)
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton2();
                break;
            case 11: // 조건: 밧줄 사용하기(EleventhStep)
                if (useRope)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton2();
                break;
        }
    }

    public void StartTutorial()
    {
        FirstStep();
        tutorialStep = 0;

        scriptManager.FindScriptByEventName("START_TUTORIAL"); // 스크립트 재생
        scriptManager.ShowScript();
    }

    private void NextStep()
    {
        switch (tutorialStep)
        {
            case 0:
                break;
            case 1:
                SecondStep();
                break;
            case 2:
                ThirdStep();
                break;
            case 3:
                FourthStep();
                break;
            case 4:
                FifthStep();
                break;
            case 5:
                SixthStep();
                break;
            case 6:
                SeventhStep();
                break;
            case 7:
                EighthStep();
                break;
            case 8:
                NinthStep();
                break;
            case 9:
                TenthStep();
                break;
            case 10:
                EleventhStep();
                break;
            case 11:
                TwelfthStep();
                break;

        }
    }

    private void FirstStep() // 좌우 이동 텍스트
    {
        tutorialPanel.SetActive(true);
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void SecondStep() // 점프 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void ThirdStep() // 아이템 습득 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void FourthStep() // 인벤토리 활성화 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
        settingButtonParticle.gameObject.SetActive(true);
        settingButtonParticle.Play();
        inventoryButtonParticle.gameObject.SetActive(true);
        inventoryButtonParticle.Play();
    }

    private void FifthStep() // 넝쿨 세 개 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
        settingButtonParticle.gameObject.SetActive(false);
        inventoryButtonParticle.gameObject.SetActive(false);
    }

    private void SixthStep() // 도구 제작 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
        settingButtonParticle.gameObject.SetActive(true);
        settingButtonParticle.Play();
        inventoryButtonParticle.gameObject.SetActive(true);
        inventoryButtonParticle.Play();
    }

    private void SeventhStep() // 크래프팅 슬롯에 넝쿨 세 개 텍스트
    {
        settingButtonParticle.gameObject.SetActive(false);
        inventoryButtonParticle.gameObject.SetActive(false);
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void EighthStep()  // 밧줄 제작 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void NinthStep() // 밧줄 제작 완료 텍스트
    {
        tutorialPanelText.text = textList[tutorialStep];
        StartCoroutine("FadeOutPanel2");
    }
    private void TenthStep() // 밧줄 사용 텍스트
    {
        arriveRopeField = false;

        tutorialPanelText.text = textList[9];

        Color c = tutorialPanel.GetComponent<Image>().color;
        Color c2 = tutorialPanelText.color;
        c.a = 1f;
        c2.a = 1f;
        tutorialPanel.GetComponent<Image>().color = c;
        tutorialPanelText.color = c2;

        settingButtonParticle.gameObject.SetActive(true);
        settingButtonParticle.Play();
        inventoryButtonParticle.gameObject.SetActive(true);
        inventoryButtonParticle.Play();

        tutorialStep++;
    }

    private void EleventhStep() // 인벤토리 밧줄 텍스트
    {
        tutorialStep++;
        tutorialPanelText.text = textList[10];
        settingButtonParticle.gameObject.SetActive(false);
        inventoryButtonParticle.gameObject.SetActive(false);
        StartCoroutine("FadeOutPanel");
    }

    private void TwelfthStep() // 튜토리얼 완료 텍스트
    {
        Color c = tutorialPanel.GetComponent<Image>().color;
        Color c2 = tutorialPanelText.color;
        c.a = 1f;
        c2.a = 1f;
        tutorialPanel.GetComponent<Image>().color = c;
        tutorialPanelText.color = c2;

        tutorialPanelText.text = textList[11];
        nextButton.SetActive(true);
    }

    // 프리스 선택
    public void SelectFrith()
    {
        tutorialPanel.SetActive(false);
        settingPanel.SetActive(false);
        selectFrithPanel.SetActive(true);
    }

    public void onClickContinueButton()
    {
        if (NEXT_STEP_POSSIBLE)
        {
            tutorialStep++;
            NextStep();
            NEXT_STEP_POSSIBLE = false;
        }
    }

    public void onClickContinueButton2()
    {
        if (NEXT_STEP_POSSIBLE)
        {
            NextStep();
            NEXT_STEP_POSSIBLE = false;
        }
    }

    public void onClickExitButton()
    {
        if (tutorialPanel.activeSelf)
            tutorialPanel.SetActive(false);
    }

    public void ReceiveRopeEvent() // playerController.UseRope.Invoke()
    {
        useRope = true;
    }

    public void ReceiveArriveEvent() // playerController.arriveRopeField.Invoke()
    {
        arriveRopeField = true;
    }

    public void OnSelectEden()
    {
        selectedCharacter = "Eden";
        frithCheckText.text = "에덴을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
        ActiveSelectMessage();
    }

    public void OnSelectNoah()
    {
        selectedCharacter = "Noah";
        frithCheckText.text = "노아를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
        ActiveSelectMessage();
    }

    public void OnSelectAdam()
    {
        selectedCharacter = "Adam";       
        frithCheckText.text = "아담을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
        ActiveSelectMessage();
    }

    public void OnSelectJonah()
    {
        selectedCharacter = "Jonah";
        frithCheckText.text = "요나를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
        ActiveSelectMessage();
    }

    private void ActiveSelectMessage()
    {
        frithCheckPanel.SetActive(true);
        selectFrithPanel.SetActive(false);
    }

    public void OnClickYesButton()
    {
        frithCheckPanel.SetActive(false);
        
        DataManager.instance.nowPlayer.nowCharacter = selectedCharacter;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);

        switch (selectedCharacter)
        {
            case "Eden":
                edenGameObject.SetActive(true);
                DataManager.instance.nowPlayer.nowCharacterInKor = "에덴";
                break;
            case "Noah":
                noahGameObject.SetActive(true);
                DataManager.instance.nowPlayer.nowCharacterInKor = "노아";
                break;
            case "Adam":
                adamGameObject.SetActive(true);
                DataManager.instance.nowPlayer.nowCharacterInKor = "아담";
                break;
            case "Jonah":
                jonahGameObject.SetActive(true);
                DataManager.instance.nowPlayer.nowCharacterInKor = "요나";
                break;
        }

        StartCoroutine("EndTutorial");
        DataManager.instance.nowPlayer.isFinishedTutorial = true;
    }

    public void OnClickNoButton()
    {
        frithCheckPanel.SetActive(false);
        selectFrithPanel.SetActive(true);
    }

    private IEnumerator EndTutorial()
    {
        yield return new WaitForSecondsRealtime(1f);

        scriptManager.FindScriptByEventName("MEET_FRITH");
        scriptManager.ShowScript();
    }

    public void FadeController()
    {
        if (scriptManager.currentScript.eventName.Equals("MEET_FRITH"))
            StartCoroutine("FadeOutScene");

        if (scriptManager.currentScript.eventName.Equals("END_TUTORIAL"))
            StartCoroutine("FadeInScene");

    }

    private IEnumerator FadeOutScene() // 튜토리얼 종료 시 페이드아웃 -> 스크립트
    {
        fader.SetActive(true);

        yield return new WaitForSecondsRealtime(1f); // 2초 뒤 페이드아웃

        Color c = fader.GetComponent<Image>().color; // 페이드아웃을 위해 Fader의 컬러값을 받아 옴

        for (float f = 0f; f <= 1f; f += 0.005f) // 페이드아웃
        {
            c.a = f;
            fader.GetComponent<Image>().color = c;
            yield return null;
        }

        scriptManager.FindScriptByEventName("END_TUTORIAL");
        scriptManager.ShowScript();
    }

    private IEnumerator FadeInScene() // 튜토리얼 종료 스크립트 재생 완료 -> 페이드인
    {
        yield return new WaitForSecondsRealtime(2f); // 2초 뒤 페이드인

        Color c = fader.GetComponent<Image>().color; // 페이드인을 위해 Fader의 컬러값을 받아 옴

        for (float f = 1f; f >= 0f; f -= 0.005f) // 페이드인
        {
            c.a = f;
            fader.GetComponent<Image>().color = c;
            yield return null;
        }

        // 빽빽한 숲에서 시작
        fader.SetActive(false);
    }

    private IEnumerator FadeOutPanel()
    {
        yield return new WaitForSecondsRealtime(3f);

        Color c = tutorialPanel.GetComponent<Image>().color;
        Color c2 = tutorialPanelText.color;

        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            c.a = f;
            c2.a = f;

            tutorialPanel.GetComponent<Image>().color = c;
            tutorialPanelText.color = c2;

            yield return null;
        }
    }

    private IEnumerator FadeInPanel()
    {
        Color c = tutorialPanel.GetComponent<Image>().color;
        Color c2 = tutorialPanelText.color;

        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            c.a = f;
            c2.a = f;

            tutorialPanel.GetComponent<Image>().color = c;
            tutorialPanelText.color = c2;

            yield return null;
        }
    }

    private IEnumerator FadeOutPanel2()
    {
        yield return new WaitForSecondsRealtime(3f);

        Color c = tutorialPanel.GetComponent<Image>().color;
        Color c2 = tutorialPanelText.color;

        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            c.a = f;
            c2.a = f;

            tutorialPanel.GetComponent<Image>().color = c;
            tutorialPanelText.color = c2;

            yield return null;
        }

        tutorialStep++;
    }
}
