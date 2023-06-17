using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithManager : MonoBehaviour
{
    public static FrithManager instance;
    private TutorialController tutorialController;


    private void Awake()
    {
        instance = this; // 싱글톤
    }

    private void Start()
    {
        tutorialController = FindObjectOfType<TutorialController>();
    }

    public void OnSelectEden()
    {
        ActiveSelectMessage();
        tutorialController.infoSmallText.text = "에덴을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectNoah()
    {
        ActiveSelectMessage();
        tutorialController.infoSmallText.text = "노아를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectAdam()
    {
        ActiveSelectMessage();
        tutorialController.infoSmallText.text = "아담을 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    public void OnSelectJonah()
    {
        ActiveSelectMessage();
        tutorialController.infoSmallText.text = "요나를 선택하시겠습니까?\n선택한 프리스는 변경할 수 없습니다.";
    }

    private void ActiveSelectMessage()
    {
        tutorialController.infoSmall.SetActive(true);
        tutorialController.Button.SetActive(true);
    }
}
