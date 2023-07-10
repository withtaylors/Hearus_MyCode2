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

    [SerializeField] private bool NEXT_STEP_POSSIBLE;

    private int tutorialStep;
    private List<string> textList = new List<string> { "방향 키를 눌러 이동할 수 있습니다.",
                                                       "스페이스바 키를 눌러 점프할 수 있습니다.",
                                                       "E 키를 눌러 아이템을 조사할 수 있습니다.",
                                                       "습득한 아이템은 인벤토리 창에서 확인할 수 있습니다.",
                                                       "이제 넝쿨을 세 개 모아 봅시다.",
                                                       "습득한 아이템으로 도구를 만들 수 있습니다. 인벤토리를 열어 보세요.",
                                                       "모은 넝쿨을 선택하여 조합할 수 있습니다. 선택 버튼을 눌러 주세요.",
                                                       "조합 버튼을 눌러 주세요.\n만들어진 도구는 인벤토리에서 확인할 수 있습니다.\n도구를 만든 재료는 사라집니다.",
                                                       "튜토리얼이 모두 끝났습니다. 함께할 파트너를 선택해 주세요." };

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialPanelText;
    public GameObject Button;
    public DialogueRunner dialogue;

    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject selectFrithPanel;
    [SerializeField] private ParticleSystem settingButtonParticle;
    [SerializeField] private ParticleSystem inventoryButtonParticle;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject fader;

    private bool right = false;
    private bool left = false;
    private bool jump = false;

    private void Start()
    {
        FirstStep();
        tutorialStep = 0;
        dialogue = FindObjectOfType<DialogueRunner>();
    }

    private void LateUpdate() // 다음 스텝으로 넘어가기 위한 조건 확인
    {
        switch (tutorialStep)
        {
            case 0: // 조건: 좌우 방향 키 입력
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    right = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    left = true;
                if (right && left)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 1: // 조건: 스페이스바 키 입력
                if (Input.GetKeyDown(KeyCode.Space))
                    jump = true;
                if (jump)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 2: // 조건: 인벤토리에 무언가를 습득
                if (Inventory.instance.inventoryItemList.Count != 0)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 3: // 조건: 인벤토리 활성화
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 4: // 조건: 넝쿨 3개 습득
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 101 && Inventory.instance.inventoryItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 5: // 조건: 인벤토리 활성화
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 6: // 조건: 크래프팅 아이템 리스트에 넝쿨 3개
                for (int i = 0; i < Crafting.instance.craftingItemList.Count; i++)
                    if (Crafting.instance.craftingItemList[i].itemID == 101 && Crafting.instance.craftingItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 7: // 조건: 밧줄 제작
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 102)
                        NEXT_STEP_POSSIBLE = true;
                onClickContinueButton();
                break;
            case 8:
                if (!NEXT_STEP_POSSIBLE)
                    NEXT_STEP_POSSIBLE = true;
                break;
        }
    }

    public void StartTutorial()
    {
        FirstStep();
        tutorialStep = 0;
    }
    
    private void NextStep()
    {
        switch(tutorialStep)
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
                EightStep();
                break;
            case 8:
                NinthStep();
                break;
            case 9:
                SelectFrith();
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

    private void EightStep()  // 인벤토리에 밧줄 확인
    {
        tutorialPanelText.text = textList[tutorialStep];
    }

    private void NinthStep()
    {
        continueButton.SetActive(true);
        tutorialPanelText.text = textList[tutorialStep];
        NEXT_STEP_POSSIBLE = true;
    }

    // 프리스 선택
    private void SelectFrith()
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

    public void onClickExitButton()
    {
        if (tutorialPanel.activeSelf)
            tutorialPanel.SetActive(false);
    }

    public void OnSelectEden()
    {
        ActiveSelectMessage();
        tutorialPanelText.text = "에덴을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectNoah()
    {
        ActiveSelectMessage();
        tutorialPanelText.text = "노아를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectAdam()
    {
        ActiveSelectMessage();
        tutorialPanelText.text = "아담을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectJonah()
    {
        ActiveSelectMessage();
        tutorialPanelText.text = "요나를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    private void ActiveSelectMessage()
    {
        tutorialPanel.SetActive(true);
        Button.SetActive(true);
    }

    public void OnClickYesButton()
    {
        tutorialPanel.SetActive(false);
        selectFrithPanel.SetActive(false);
        dialogue.StartDialogue("AfterSelectFrith");
    }

    public void OnClickNoButton()
    {
        tutorialPanel.SetActive(false);
        Button.SetActive(false);
    }

    private IEnumerator FadeOutStart() // 튜토리얼 종료 시 페이드아웃 -> 스크립트
    {
        yield return new WaitForSecondsRealtime(2f); // 2초 뒤 페이드아웃

        Color c = fader.GetComponent<Image>().color; // 페이드아웃을 위해 Fader의 컬러값을 받아 옴

        for (float f = 0f; f <= 1f; f += 0.01f) // 페이드아웃
        {
            c.a = f;
            fader.GetComponent<Image>().color = c;
            yield return null;
        }

        dialogue.StartDialogue("AfterTutorial");
    }
}
