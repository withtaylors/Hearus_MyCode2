using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Yarn.Unity;
using static UnityEngine.ParticleSystem;

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
                                                       "습득한 아이템으로 도구를 만들 수 있습니다.",
                                                       "만들어진 도구는 인벤토리에서 확인할 수 있습니다.\n도구를 만든 재료는 사라집니다."};

    [SerializeField] private GameObject infoSmall;
    [SerializeField] private GameObject infoLarge;
    [SerializeField] private TextMeshProUGUI infoSmallText;
    [SerializeField] private TextMeshProUGUI infoLargeText;
    [SerializeField] private GameObject keyboardImage;
    [SerializeField] private ParticleSystem settingButtonParticle;
    [SerializeField] private ParticleSystem inventoryButtonParticle;

    private bool right = false;
    private bool left = false;
    private bool jump = false;

    private void Start()
    {
        FirstStep();
        tutorialStep = 0;
    }

    private void LateUpdate() // 다음 스텝으로 넘어가기 위한 조건 확인
    {
        switch (tutorialStep)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    right = true;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    left = true;
                if (right && left)
                    NEXT_STEP_POSSIBLE = true;
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                    jump = true;
                if (jump)
                    NEXT_STEP_POSSIBLE = true;
                break;
            case 2:
                if (Inventory.instance.inventoryItemList.Count != 0)
                    NEXT_STEP_POSSIBLE = true;
                break;
            case 3:
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                break;
            case 4:
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 101 && Inventory.instance.inventoryItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                break;
            case 5:
                if (Inventory.instance.go_Inventory.activeSelf)
                    NEXT_STEP_POSSIBLE = true;
                break;
            case 6:
                for (int i = 0; i < Crafting.instance.craftingItemList.Count; i++)
                    if (Crafting.instance.craftingItemList[i].itemID == 101 && Crafting.instance.craftingItemList[i].itemCount == 3)
                        NEXT_STEP_POSSIBLE = true;
                break;
            case 7:
                for (int i = 0; i < Inventory.instance.inventoryItemList.Count; i++)
                    if (Inventory.instance.inventoryItemList[i].itemID == 102)
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
        }
    }

    private void FirstStep() // 좌우 이동 확인
    {
        infoLarge.SetActive(true);
        infoLargeText.text = textList[tutorialStep];
    }

    private void SecondStep() // 점프 확인
    {
        infoLargeText.text = textList[tutorialStep];
    }

    private void ThirdStep() // 아이템 습득 확인
    {
        infoLarge.SetActive(false);
        infoSmall.SetActive(true);
        infoSmallText.text = textList[tutorialStep];
    }

    private void FourthStep() // 인벤토리 활성화 확인
    {
        infoSmallText.text = textList[tutorialStep];
        settingButtonParticle.gameObject.SetActive(true);
        settingButtonParticle.Play();
        inventoryButtonParticle.gameObject.SetActive(true);
        inventoryButtonParticle.Play();
    }

    private void FifthStep() // 넝쿨 세 개 확인
    {
        infoSmallText.text = textList[tutorialStep];
        settingButtonParticle.Pause();
    }

    private void SixthStep() // 인벤토리 활성화 확인
    {
        settingButtonParticle.Play();
        inventoryButtonParticle.Play();
    }

    // 크래프팅 슬롯에 넝쿨 세 개 확인

    // 인벤토리에 밧줄 확인

    // 마지막 멘트, 튜토리얼 끝

    public void onClickContinueButton()
    {
        if (tutorialStep == textList.Count - 1)
            return;
        else
            if (NEXT_STEP_POSSIBLE)
            {
                tutorialStep++;
                NextStep();
                NEXT_STEP_POSSIBLE = false;
            }
    }

    public void onClickExitButton()
    {
        if (infoSmall.activeSelf)
            infoSmall.SetActive(false);
        else if (infoLarge.activeSelf)
            infoLarge.SetActive(false);
    }

    ///
    /// 1. 첫 번째 스텝
    ///     small 패널 활성화, 인삿말 출력, continue->두 번째 스텝으로 넘어감
    /// 2. 두 번째 스텝
    ///     large 패널 활성화, 이동 안내 문구 출력, 상하좌우 입력 확인 시 세 번째 스텝으로 넘어감
    /// 3. 세 번째 스텝
    ///     점프 안내 문구 출력, 스페이스바 확인 시 네 번째 스텝으로 넘어감
    ///
}
