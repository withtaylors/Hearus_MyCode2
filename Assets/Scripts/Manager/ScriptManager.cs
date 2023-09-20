using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager instance;

    [SerializeField] private GameObject nextButton; // 다음 버튼
    [SerializeField] private GameObject go_ScriptPanel; // 스크립트 패널
    [SerializeField] private TextMeshProUGUI scriptText; // 스크립트 텍스트
    [SerializeField] private GameObject go_OptionView; // 옵션 리스트
    [SerializeField] private List<GameObject> optionPrefab = new List<GameObject>(); // 옵션 버튼 프리팹
    [SerializeField] private List<string> optionText = new List<string>(); // 옵션 텍스트
    [SerializeField] private ScriptEvent script; // 스크립트
    [SerializeField] private OptionEvent option; // 스크립트

    private Script[] scripts; // 전체 스크립트 목록을 받아 올 배열
    public Script currentScript; // 현재 스크립트를 받아 옴

    private Option[] options;
    public Option currentOption; // 현재 옵션을 받아 옴

    private ScriptManager scriptManager;

    public bool isPlayingScript = false; // 스크립트 재생 중일 경우 true
    public bool isFinished = false; // 스크립트 재생이 끝나면 true
    public bool isTyping = false; // 타이핑 중이면 true
    private bool isNext = false; // 특정 키 입력 대기. true가 되면 키 입력 가능
    private int currentLine = 0;
    public GameObject currentGameObject;

    public UnityEvent FinishedScript;

    int totalCharacters;
    Coroutine myCoroutine;

    private void Start()
    {
        instance = this;

        scriptManager = FindObjectOfType<ScriptManager>();
        scriptManager.LoadScript(scriptManager.GetScript());
        scriptManager.LoadOption(scriptManager.GetOption());
    }

    private void Update()
    {
        if (isPlayingScript)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (isTyping)
                    {
                        StopCoroutine(myCoroutine);
                        scriptText.maxVisibleCharacters = scriptText.text.Length;
                        isTyping = false;
                        nextButton.SetActive(true);
                    }
                    else
                    {
                        nextButton.SetActive(false);
                        isNext = false;
                        if (++currentLine < currentScript.sentences.Length)
                        {
                            //문장이 남아 있을 때
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            if (currentScript.isExistOption == "Y")
                            {
                                //옵션이 있을 때
                                FindOption(int.Parse(currentScript.optionNumber));
                                ShowOption();
                            }
                            else if (currentScript.isExistNextScript == "Y")
                            {
                                //다음 스크립트가 있을 때
                                JumpToNextScript();
                            }

                            else
                            {
                                //아무것도 없을 때
                                if (currentScript.itemID != "")
                                {
                                    currentGameObject.GetComponent<ItemPickup>().Pickup();
                                    currentGameObject.SetActive(false); // 습득 후 비활성화시킴
                                }
                                ShowScriptUI(false);
                                isFinished = true;
                                isPlayingScript = false;
                                FinishedScript.Invoke();
                            }
                        }
                    }
                }
            }
        }

        if (go_OptionView.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (EventSystem.current.currentSelectedGameObject == optionPrefab[0])
                    EventSystem.current.SetSelectedGameObject(optionPrefab[1]);
                else if (EventSystem.current.currentSelectedGameObject == optionPrefab[1])
                    EventSystem.current.SetSelectedGameObject(optionPrefab[2]);
                else if (EventSystem.current.currentSelectedGameObject == optionPrefab[2])
                    EventSystem.current.SetSelectedGameObject(optionPrefab[0]);
            }
        }
    }

    IEnumerator TypeWriter()
    {
        isFinished = false;
        isTyping = true;

        scriptText.DOKill();
        scriptText.text = "";

        yield return new WaitForSeconds(0.5f);

        string t_ReplaceText = currentScript.sentences[currentLine];
        t_ReplaceText = t_ReplaceText.Replace("'", ","); // csv 파일에는 쉼표가 들어가면 안 되므로 '를 ,로 치환해 줌
        scriptText.text = t_ReplaceText;

        ShowScriptUI(true);

        myCoroutine = StartCoroutine(ShowTextCoroutine(scriptText.text, 0.055f));

        isNext = true;
    }

    IEnumerator OptionView()
    {
        optionText.Clear();

        ShowOptionUI(true);

        for (int i = 0; i < currentOption.sentences.Length; i++)
        {
            optionText.Add(currentOption.sentences[i]);

            string replacedText = currentOption.sentences[i].Replace("'", ",");
            optionText[i] = replacedText;

            optionPrefab[i].GetComponentInChildren<TextMeshProUGUI>().text = optionText[i];
            optionPrefab[i].SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(optionPrefab[0]);
        yield return new WaitForSeconds(0.1f);
    }

    public void LoadScript(Script[] p_scripts)
    {
        scripts = p_scripts;
    }

    public void LoadOption(Option[] p_options)
    {
        options = p_options;
    }

    public void ShowScript()
    {
        isPlayingScript = true;
        currentLine = 0;

        if (currentScript.scriptSwitch != string.Empty)
            SwitchOn(currentScript.scriptSwitch);

        StartCoroutine(TypeWriter());
    }

    public void ShowOption()
    {
        optionText.Clear();

        StartCoroutine(OptionView());
    }

    public Script[] GetScript() // 스크립트 파일의 x줄부터 y줄까지 불러오기
    {
        script.scripts = DatabaseManager.instance.GetScript((int)script.line.x, (int)script.line.y);
        return script.scripts;
    }

    public Option[] GetOption() // 옵션 파일의 x줄부터 y줄까지 불러오기
    {
        option.options = DatabaseManager.instance.GetOption((int)option.line.x, (int)option.line.y);
        return option.options;
    }

    public void FindScriptByItemID(int _itemID) // 아이템 아이디로 스크립트를 검색해 currentScript에 넣음
    {
        for (int i = 0; i < script.scripts.Length; i++)
            if (script.scripts[i].itemID == _itemID.ToString())
                currentScript = script.scripts[i];
        currentLine = 0;
    }

    public void FindScriptByScriptID(int _scriptID) // 스크립트 아이디로 스크립트를 검색해 currentScript에 넣음
    {
        for (int i = 1; i <= script.scripts.Length; i++)
        {
            //if (script.scripts[i].scriptID == _scriptID)
            if (i == _scriptID)
            {
                currentScript = script.scripts[i - 1];
                break;
            }
        }
        currentLine = 0;
    }

    public void FindScriptByEventName(string _eventName) // 아이템과 관련된 스크립트가 아닐 때는 이벤트 이름으로 검색함
    {
        for (int i = 0; i < script.scripts.Length; i++)
        {
            if (script.scripts[i].eventName == _eventName)
            {
                currentScript = script.scripts[i];
                break;
            }
        }
        currentLine = 0;
    }

    public void FindOption(int optionNum)
    {
        for (int i = 0; i < option.options.Length; i++)
            if (int.Parse(option.options[i].optionID) == optionNum)
                currentOption = option.options[i];
    }

    private void ShowScriptUI(bool p_flag)
    {
        go_ScriptPanel.SetActive(p_flag);
    }

    private void ShowOptionUI(bool p_flag) 
    {
        for (int i = 0; i < optionPrefab.Count; i++)
            optionPrefab[i].SetActive(false);
        go_OptionView.SetActive(p_flag);
    }

    private void JumpToNextScript()
    {
        Debug.Log("JumpToNextScript()");
        FindScriptByScriptID(int.Parse(currentScript.nextScriptNumber));
        ShowScript();
    }

    private void SwitchOn(string _switch) // 스크립트 조건 제어 스위치
    {
        for (int i = 0; i < ScriptSwitch.instance.switchs.Count; i++)
            if (ScriptSwitch.instance.switchs[i].switchName == _switch)
                ScriptSwitch.instance.switchs[i].switchValue = true;

    }

    public void OnOptionButtonClick()
    {
        string buttonTag = EventSystem.current.currentSelectedGameObject.tag; // 선택한 옵션의 태그를 받아 옴

        if (buttonTag == "FirstOption")
        {
            // 선택한 옵션에 따른 HP 증감
            if (currentOption.optionEffect[0] == "피해")
                PlayerHP.instance.DecreaseHP(int.Parse(currentOption.optionEffectValue[0]));
            else if (currentOption.optionEffect[0] == "회복")
                PlayerHP.instance.IncreaseHP(int.Parse(currentOption.optionEffectValue[0]));

            FindScriptByScriptID(int.Parse(currentOption.nextScriptNumber[0]));
            ShowScript();
        }
        else if (buttonTag == "SecondOption")
        {
            // 선택한 옵션에 따른 HP 증감
            if (currentOption.optionEffect[1] == "피해")
                PlayerHP.instance.DecreaseHP(int.Parse(currentOption.optionEffectValue[1]));
            else if (currentOption.optionEffect[1] == "회복")
                PlayerHP.instance.IncreaseHP(int.Parse(currentOption.optionEffectValue[1]));

            FindScriptByScriptID(int.Parse(currentOption.nextScriptNumber[1]));
            ShowScript();
        }
        else if (buttonTag == "ThirdOption")
        {
            // 선택한 옵션에 따른 HP 증감
            if (currentOption.optionEffect[2] == "피해")
                PlayerHP.instance.DecreaseHP(int.Parse(currentOption.optionEffectValue[2]));
            else if (currentOption.optionEffect[2] == "회복")
                PlayerHP.instance.IncreaseHP(int.Parse(currentOption.optionEffectValue[2]));

            FindScriptByScriptID(int.Parse(currentOption.nextScriptNumber[2]));
            ShowScript();
        }

        ShowOptionUI(false);
    }

    public static void DoText(TextMeshProUGUI _text, float _duration)
    {
        _text.maxVisibleCharacters = 0;

        int totalCharacters = _text.text.Length;
        int charactersToShow = 1;
        float dynamicDuration = _duration / totalCharacters;

        DOTween.To(() => _text.maxVisibleCharacters, x => _text.maxVisibleCharacters = x, totalCharacters, _duration)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnUpdate(() =>
            {
                if (_text.maxVisibleCharacters >= charactersToShow)
                {
                    charactersToShow++;
                    DOTween.To(() => _text.maxVisibleCharacters, x => _text.maxVisibleCharacters = x, charactersToShow, dynamicDuration)
                        .SetEase(Ease.Linear);
                }
            });
    }

    private IEnumerator ShowTextCoroutine(string completeText, float timePerCharacter)
    {
        scriptText.maxVisibleCharacters = 0;

        int totalCharacters = completeText.Length;

        for (int i = 0; i <= totalCharacters; i++)
        {
            scriptText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(timePerCharacter);
        }

        if (isTyping) isTyping = false;
        nextButton.SetActive(true);
    }

    public void OnNextButtonClick()
    {
        isNext = false;
        if (++currentLine < currentScript.sentences.Length)
        {
            //문장이 남아 있을 때
            StartCoroutine(TypeWriter());
        }
        else
        {
            if (currentScript.isExistOption == "Y")
            {
                //옵션이 있을 때
                FindOption(int.Parse(currentScript.optionNumber));
                ShowOption();
            }
            else if (currentScript.isExistNextScript == "Y")
            {
                //다음 스크립트가 있을 때
                JumpToNextScript();
            }

            else
            {
                //아무것도 없을 때
                ShowScriptUI(false);
                isFinished = true;
                isPlayingScript = false;
                FinishedScript.Invoke();
            }
        }
        nextButton.SetActive(false);
    }
}