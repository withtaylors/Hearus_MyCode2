using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ScriptManager : MonoBehaviour
{
    [SerializeField] private GameObject go_ScriptPanel; // 스크립트 패널
    [SerializeField] private TextMeshProUGUI scriptText; // 스크립트 텍스트
    [SerializeField] private GameObject go_OptionView; // 옵션 리스트
    [SerializeField] private List<GameObject> optionPrefab = new List<GameObject>(); // 옵션 버튼 프리팹
    [SerializeField] private List<string> optionText = new List<string>(); // 옵션 텍스트
    [SerializeField] private ScriptEvent script; // 스크립트
    [SerializeField] private OptionEvent option; // 스크립트

    private Script[] scripts; // 전체 스크립트 목록을 받아 올 배열
    private Script currentScript; // 현재 스크립트를 받아 옴

    private Option[] options;
    private Option currentOption; // 현재 옵션을 받아 옴

    bool isDialogue = false; // 스크립트 재생 중일 경우 true
    bool isNext = false; // 특정 키 입력 대기. true가 되면 키 입력 가능

    int currentLine = 0;

    private ScriptManager scriptManager;

    private void Start()
    {
        scriptManager = FindObjectOfType<ScriptManager>();
        scriptManager.LoadScript(scriptManager.GetScript());
        scriptManager.LoadOption(scriptManager.GetOption());
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isNext = false;
                    if (++currentLine < currentScript.sentences.Length)
                        StartCoroutine(TypeWriter());
                    else
                    {
                        if (currentScript.isExistOption == "Y")
                        {
                            FindOption(currentScript.optionNumber);
                            ShowOption();
                        }
                    }
                }
            }
        }
    }

    IEnumerator TypeWriter()
    {
        ShowScriptUI(true);

        string t_ReplaceText = currentScript.sentences[currentLine];
        t_ReplaceText = t_ReplaceText.Replace("'", ","); // csv 파일에는 쉼표가 들어가면 안 되므로 '를 ,로 치환해 줌

        scriptText.text = t_ReplaceText;

        isNext = true;
        yield return null;
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

        yield return null;
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
        isDialogue = true;
        currentLine = 0;
        scriptText.text = "";

        StartCoroutine(TypeWriter());
    }

    public void ShowOption()
    {
        optionText.Clear();

        StartCoroutine(OptionView());
    }

    public Script[] GetScript()
    {
        script.scripts = DatabaseManager.instance.GetScript((int)script.line.x, (int)script.line.y);
        return script.scripts;
    }

    public Option[] GetOption()
    {
        option.options = DatabaseManager.instance.GetOption((int)option.line.x, (int)option.line.y);
        return option.options;
    }

    public void FindScriptByItemID(int _itemID)
    {
        for (int i = 0; i < script.scripts.Length; i++)
            if (script.scripts[i].itemID == _itemID.ToString())
                currentScript = script.scripts[i];
    }

    public void FindScriptByScriptID(string _scriptID)
    {
        for (int i = 0; i < script.scripts.Length; i++)
            if (script.scripts[i].scriptID == _scriptID.ToString())
                currentScript = script.scripts[i];
    }

    public void FindOption(string optionNum)
    {
        for (int i = 0; i < option.options.Length; i++)
            if (option.options[i].optionID == optionNum)
                currentOption = option.options[i];

        ShowOptionUI(true);
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

    }

    public void OnClickOptionButton()
    {
        if (EventSystem.current.currentSelectedGameObject.CompareTag("FirstOption"))
        {
            FindScriptByScriptID(currentOption.nextScriptNumber[0]);
            StartCoroutine(TypeWriter());

        } else if (EventSystem.current.currentSelectedGameObject.CompareTag("SecondOption"))
        {
            FindScriptByScriptID(currentOption.nextScriptNumber[1]);
            StartCoroutine(TypeWriter());
        } else if (EventSystem.current.currentSelectedGameObject.CompareTag("ThirdOption"))
        {
            FindScriptByScriptID(currentOption.nextScriptNumber[2]);
            StartCoroutine(TypeWriter());
        }
    }
}
