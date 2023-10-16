using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class JourneyList
{
    public Journey _journey;
    public Transform _map;

    public JourneyList (Journey journey, Transform map)
    {
        _journey = journey;
        _map = map;
    }
}

public class JourneyManager : MonoBehaviour
{
    public static JourneyManager instance;

    private JourneyManager journeyManager;

    [SerializeField] private JourneyEvent journey;

    private Journey[] journeys;
    public Journey currentJourney;

    public List<JourneyList> _journeyList;

    public Transform firstPage;
    public Transform secondPage;
    public Transform thirdPage;
    public Transform fourthPage;
    public Transform fifthPage;

    public Transform currentPage;

    public GameObject JourneySlot;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        journeyManager = FindObjectOfType<JourneyManager>();
        journeyManager.LoadJourney(journeyManager.GetJourney());

        _journeyList = new List<JourneyList>();
    }

    public void LoadJourney(Journey[] p_journeys)
    {
        journeys = p_journeys;
    }

    public Journey[] GetJourney()
    {
        journey.journeys = JourneyDataManager.instance.GetJourney((int)journey.line.x, (int)journey.line.y);
        return journey.journeys;
    }

    public void FIndJourneyByScriptID(string _scriptID)
    {
        for (int i = 0; i < journey.journeys.Length; i++)
        {
            if (journey.journeys[i].scriptNumber == _scriptID)
            {
                currentJourney = journey.journeys[i];
                return;
            }
        }
        Debug.Log("일치하는 일지 번호 없음");
    }

    public void FIndJourneyByJourneyNumber(string _journeyNumber)
    {
        for (int i = 0; i < journey.journeys.Length; i++)
        {
            if (journey.journeys[i].journeyNumber.Equals(_journeyNumber))
            {
                currentJourney = journey.journeys[i];
                return;
            }
        }
        Debug.Log("일지 못찾음");
    }

    public void FIndJourneyByJourneyNumber2()
    {
        Debug.Log(ScriptManager.instance.currentScript.journeyNumber);

        for (int i = 0; i < journey.journeys.Length; i++)
        {
            if (journey.journeys[i].journeyNumber == ScriptManager.instance.currentScript.journeyNumber)
            {
                currentJourney = journey.journeys[i];
                return;
            }
        }

        Debug.Log("일지 못찾음");
    }

    // 스크립트 매니저에서 AddJourney 이벤트 Invoke를 받는 함수
    public void UpdateJourney()
    {   
        GetCurrentScene(); // 현재 씬 이름 받아 오기

        for (int i = 0; i < _journeyList.Count; i++) // 중복 검사
            if (_journeyList[i]._journey.journeyNumber == currentJourney.journeyNumber) // 중복 시 리턴
                return;

        for (int i = 0; i < _journeyList.Count; i++) // 해당 아이템이 이미 있는지 검사
            if (currentJourney.journeyID == _journeyList[i]._journey.journeyID)
            {
                AddJourneyString(i);
                return;
            }

        _journeyList.Add(new JourneyList(currentJourney, currentPage)); // 아니라면 일지 리스트에 추가

        GameObject journeyObject = Instantiate(JourneySlot, currentPage); // 일지 오브젝트 생성

        journeyObject.name = currentJourney.journeyID; // 일지 오브젝트의 이름은 일지 아이디로 지정

        TextMeshProUGUI journeyName = journeyObject.transform.Find("Journey Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI journeyText = journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>();

        //FIndJourneyByScriptID(ScriptManager.instance.currentScript.scriptID);

        journeyName.text = currentJourney.journeyName;
        journeyText.text = currentJourney.journeyString[0];

        if (currentJourney.journeyString.Length > 1) // 일지 텍스트의 개수가 하나 이상이라면
            for (int i = 1; i < currentJourney.journeyString.Length; i++)
                journeyText.text = journeyText.text + "\n" + currentJourney.journeyString[i]; // 줄바꿈 뒤 이어 붙임

        // 스토리 일지의 경우 분홍색으로 표시
        if (currentJourney.journeyType == "STORY")
            journeyName.color = new Color32(252, 173, 244, 255);

        // 작은따옴표를 쉼표로 치환
        string replaceText = journeyText.text;
        replaceText = replaceText.Replace("'", ",");
        journeyText.text = replaceText;

        replaceText = replaceText.Replace("ㅇㅇㅇ", DataManager.instance.nowPlayer.nowCharacterInKor);
        journeyText.text = replaceText;
    }

    public void AddJourneyString(int i)
    {
        _journeyList.Add(new JourneyList(currentJourney, currentPage));

        GameObject _journeyObject = _journeyList[i]._map.Find(currentJourney.journeyID).gameObject;        
        TextMeshProUGUI _journeyText = _journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>();

        _journeyText.text = _journeyText.text + "\n" + currentJourney.journeyString[0];

        if (currentJourney.journeyString.Length > 1) // 일지 텍스트의 개수가 하나 이상이라면
            for (int j = 1; j < currentJourney.journeyString.Length; j++)
                _journeyText.text = _journeyText.text + "\n" + currentJourney.journeyString[j]; // 줄바꿈 뒤 이어 붙임

        string replaceText = _journeyText.text;
        replaceText = replaceText.Replace("'", ",");
        _journeyText.text = replaceText;
    }

    public void GetCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("태초의숲"))
            currentPage = firstPage;
        else if (scene.name.Equals("비탄의바다"))
            currentPage = secondPage;
        else if (scene.name.Equals("타오르는황야"))
            currentPage = thirdPage;
    }
}
