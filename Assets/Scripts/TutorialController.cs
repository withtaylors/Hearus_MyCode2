using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TutorialController : MonoBehaviour
{
    /// <summary>
    /// 수정중!!!!!!!
    /// </summary>

    private bool NEXT_STEP_POSSIBLE;
    private int tutorialStep;
    private static List<string> textList = new List<string> { "안녕하세요, 모험가님!\n히얼어스에 오신 것을 환영합니다.",
                                                              "이곳에서는 키보드의 상하좌우 키로 이동할 수 있습니다.\n한번 해 볼까요?" };

    [SerializeField] private GameObject infoSmall;
    [SerializeField] private GameObject infoLarge;
    [SerializeField] private TextMeshProUGUI infoSmallText;
    [SerializeField] private TextMeshProUGUI infoLargeText;
    [SerializeField] private GameObject keyboardImage;
    
    private void Start()
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
        }
    }

    private void FirstStep()
    {
        infoSmall.SetActive(true);

        infoSmallText.text = textList[tutorialStep];

        NEXT_STEP_POSSIBLE = true;
    }

    private void SecondStep()
    {
        NEXT_STEP_POSSIBLE = false;

        infoSmall.SetActive(false);

        infoLarge.SetActive(true);
        keyboardImage.SetActive(true);

        infoLargeText.text = textList[tutorialStep];

        // if (상하좌우 입력 다 확인) NEXT_STEP_POSSIBLE, onClickContinueButton();
    }

    public void onClickContinueButton()
    {
        if (tutorialStep == textList.Count - 1)
            return;
        else
            if (NEXT_STEP_POSSIBLE)
            {
                tutorialStep++;
                NextStep();
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
