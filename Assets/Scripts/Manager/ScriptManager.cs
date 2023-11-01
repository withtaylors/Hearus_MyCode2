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
using Unity.VisualScripting;

public class ScriptManager : MonoBehaviour
{
    public static ScriptManager instance;

    private JourneyManager journeyManager;

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
    public UnityEvent AddJourney;

    int totalCharacters;
    Coroutine myCoroutine;

    public AudioSource typingSound;
    public Slider volumeSlider; // 볼륨 슬라이더

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        scriptManager = FindObjectOfType<ScriptManager>();
        scriptManager.LoadScript(scriptManager.GetScript());
        scriptManager.LoadOption(scriptManager.GetOption());

        journeyManager = FindObjectOfType<JourneyManager>();

        if (DataManager.instance.nowPlayer.firstStart)
            DataManager.instance.nowPlayer.firstStart = false;

    }

    private void Update()
    {
        ReturnScript();

        if (isTyping && !isFinished)
        {
            // 타이핑 중인 경우 소리 재생
            if (!typingSound.isPlaying)
            {
                // 볼륨 슬라이더 값에 따라 볼륨 조절
                typingSound.volume = volumeSlider.value;
                typingSound.Play();
            }
        }
        else
        {
            // 타이핑 중이 아니거나 스크립트가 끝났을 때 소리 정지 및 서서히 줄이기
            if (typingSound.isPlaying)
            {
                // 서서히 볼륨 감소
                StartCoroutine(FadeOutVolume(typingSound, volumeSlider.value, 1.0f)); // 1.0f는 볼륨 감소에 걸리는 시간
            }
        }
    }

    // 볼륨 서서히 감소시킴
    private IEnumerator FadeOutVolume(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > targetVolume)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void ReturnScript()
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
                            // 문장이 남아 있을 때
                            StartCoroutine(TypeWriter());
                        }
                        else
                        {
                            if (currentScript.present != "")
                            {
                                // 프리스 선물 버튼을 눌렀을 때
                                PresentToFrith.instance.OnPresentChoice(currentScript.present);
                            }

                            if (currentScript.isExistOption == "Y")
                            {
                                // 옵션이 있을 때
                                FindOption(int.Parse(currentScript.optionNumber));
                                ShowOption();
                            }
                            else if (currentScript.isExistNextScript == "Y")
                            {
                                // 다음 스크립트가 있을 때
                                JumpToNextScript();
                            }
                            else
                            {
                                // 아무것도 없을 때
                                if (currentScript.getTiming != "")
                                {
                                    if (currentGameObject != null)
                                    {
                                        currentGameObject.GetComponent<ItemPickup>().Pickup();
                                        currentGameObject.SetActive(false); // 습득 후 비활성화시킴
                                    }
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

        t_ReplaceText = t_ReplaceText.Replace("ㅇㅇㅇ", DataManager.instance.nowPlayer.nowCharacterInKor);
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

        if (currentOption.sentences.Length == 3)
            if (!DataManager.instance.nowPlayer.isFinishedTutorial) // 만약 튜토리얼을 끝내지 않은 상태라면
                optionPrefab[1].SetActive(false); // '프리스에게 준다' 선택지를 비활성화시킴

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
        {
            if (script.scripts[i].itemID == _itemID.ToString())
            {
                currentScript = script.scripts[i];
                FIndJourney();
                break;
            }
        }
        currentLine = 0;
    }

    public void FindScriptByScriptID(int _scriptID)
    {
        for (int i = 0; i < script.scripts.Length; i++)
        {
            if (script.scripts[i].scriptID == _scriptID.ToString())
            {
                currentScript = script.scripts[i];
                FIndJourney();
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
                FIndJourney();
                break;
            }
        }
        currentLine = 0;
    }

    public void FindScriptByItemDesNum(int _itemDes) // 이미 획득한 적 있는 아이템은 아이템의 설명을 나타내는 스크립트만 재생
    {
        for (int i = 0; i < script.scripts.Length; i++)
        {
            if (script.scripts[i].itemDes == _itemDes.ToString())
            {
                currentScript = script.scripts[i];
                break;
            }
        }
        currentLine = 0;
    }

    public void FIndJourney() // 해당 스크립트에 일지가 있는지
    {
        Debug.Log("FIndJourney() 호출");
        if (!string.IsNullOrEmpty(currentScript.journeyNumber)) // 일지 번호 항목이 공백이 아니라면
        {
            Debug.Log("currentScript.journeyNumber: " + currentScript.journeyNumber);

            for (int i = 0; i < DataManager.instance.dataWrapper.getItemIDList.Count; i++)
            {
                if (currentScript.itemID == DataManager.instance.dataWrapper.getItemIDList[i].ToString()) // 획득 이력이 있는 아이템이라면 리턴
                {
                    Debug.Log("획득 이력이 있기 때문에 리턴");
                    return; 
                }
                    
            }

            Debug.Log("획득 이력이 없음, 일지 추가");
            journeyManager.FIndJourneyByJourneyNumber(currentScript.journeyNumber); // 획득 이력이 없다면 currentJourney에 일지 번호를 넣음
            journeyManager.UpdateJourney();                                         // 일지 추가
        }
        else // 공백이라면 return
            return;
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
        int buttonNum = -1;
        
        switch (buttonTag)
        {
            case "FirstOption":
                buttonNum = 0;
                break;
            case "SecondOption":
                buttonNum = 1;
                break;
            case "ThirdOption":
                buttonNum = 2;
                break;
        }

        if (currentOption.optionID == "39") // 최종 선택지
        {
            if (buttonNum == 0)
            {
                // 프리스를 데려가는 선택지를 골랐을 때
                ShowOptionUI(false);
                return;
            }
            else if (buttonNum == 1)
            {
                // 프리스를 데려가지 않는 선택지를 골랐을 때
                ShowOptionUI(false);
                return;
            }
        }

        if (currentOption.optionEffect[buttonNum] == "피해")
        {
            PlayerHP.instance.DecreaseHP(int.Parse(currentOption.optionEffectValue[buttonNum]));
            PlayerHP.HPDecreased = true;
        }
        else if (currentOption.optionEffect[buttonNum] == "회복")
        {
            PlayerHP.instance.IncreaseHP(int.Parse(currentOption.optionEffectValue[buttonNum]));
            PlayerHP.HPIncreased = true;
        }

        if (currentOption.isExistNextScript[buttonNum] != "Y")
        {
            FindScriptByScriptID(int.Parse(currentOption.nextScriptNumber[buttonNum]));
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