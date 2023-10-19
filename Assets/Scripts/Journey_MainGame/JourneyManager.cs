using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class JourneyManager : MonoBehaviour
{
    public static JourneyManager instance;

    private JourneyManager journeyManager;

    [SerializeField] private JourneyEvent journey;

    private Journey[] journeys;
    public Journey currentJourney;

    public Transform firstPage;
    public Transform secondPage;
    public Transform thirdPage;
    public Transform fourthPage;
    public Transform fifthPage;

    public Transform currentPage;
    public string currentPageName;

    public GameObject JourneySlot;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        journeyManager = FindObjectOfType<JourneyManager>();
        journeyManager.LoadJourney(journeyManager.GetJourney());

        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "태초의숲" || scene.name == "비탄의바다" || scene.name == "타오르는황야")
            ChangeMap();
        else
            return;
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
        Scene scene = SceneManager.GetActiveScene();
        GetCurrentScene(scene.name); // 현재 씬 이름 받아 오기

        for (int i = 0; i < JourneyDataManager.instance._journeyList.Count; i++) // 중복 검사
            if (JourneyDataManager.instance._journeyList[i]._journey.journeyNumber == currentJourney.journeyNumber) // 중복 시 리턴
                return;

        for (int i = 0; i < JourneyDataManager.instance._journeyList.Count; i++) // 해당 아이템이 이미 있는지 검사
            if (currentJourney.journeyID == JourneyDataManager.instance._journeyList[i]._journey.journeyID)
            {
                AddJourneyString(i);
                return;
            }

        JourneyDataManager.instance._journeyList.Add(new JourneyList(currentJourney, scene.name)); // 아니라면 일지 리스트에 추가

        GameObject journeyObject = Instantiate(JourneySlot, currentPage); // 일지 오브젝트 생성

        journeyObject.name = currentJourney.journeyID; // 일지 오브젝트의 이름은 일지 아이디로 지정

        TextMeshProUGUI journeyName = journeyObject.transform.Find("Journey Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI journeyText = journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>();

        //FIndJourneyByScriptID(ScriptManager.instance.currentScript.scriptID);

        journeyName.text = currentJourney.journeyName;
        journeyText.text = currentJourney.journeyString;

        // 스토리 일지의 경우 분홍색으로 표시
        if (currentJourney.journeyType == "STORY")
            journeyName.color = new Color32(252, 173, 244, 255);

        // 치환
        string replaceText = journeyText.text;

        replaceText = replaceText.Replace("'", ",");
        replaceText = replaceText.Replace("ㅇㅇㅇ", DataManager.instance.nowPlayer.nowCharacterInKor);
        replaceText = replaceText.Replace("\\n", "\n");

        journeyText.text = replaceText;
    }

    public void AddJourneyString(int i)
    {
        bool existJourneyList = false;

        for (int j = 0; j < JourneyDataManager.instance._journeyList.Count; j++)
            if (JourneyDataManager.instance._journeyList[j]._journey.journeyNumber == currentJourney.journeyNumber)
            {
                existJourneyList = true;
                break;
            }

        if (!existJourneyList)
        {
            Scene scene = SceneManager.GetActiveScene();
            GetCurrentScene(scene.name); // 현재 씬 이름 받아 오기
            JourneyDataManager.instance._journeyList.Add(new JourneyList(currentJourney, scene.name));
        }

        GameObject _journeyObject = new GameObject();

        switch (JourneyDataManager.instance._journeyList[i]._map)
        {
            case "태초의숲":
                _journeyObject = firstPage.Find(currentJourney.journeyID).gameObject;
                break;
            case "비탄의바다":
                _journeyObject = secondPage.Find(currentJourney.journeyID).gameObject;
                break;
            case "타오르는황야":
                _journeyObject = thirdPage.Find(currentJourney.journeyID).gameObject;
                break;
        }

        //GameObject _journeyObject = JourneyDataManager.instance._journeyList[i]._map.Find(currentJourney.journeyID).gameObject;
        //
        TextMeshProUGUI _journeyText = _journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>();

        _journeyText.text = _journeyText.text + "\n" + currentJourney.journeyString;

        // 치환
        string replaceText = _journeyText.text;

        replaceText = replaceText.Replace("'", ",");
        replaceText = replaceText.Replace("ㅇㅇㅇ", DataManager.instance.nowPlayer.nowCharacterInKor);
        replaceText = replaceText.Replace("\\n", "\n");

        _journeyText.text = replaceText;
    }

    public void GetCurrentScene(string _string)
    {
        switch(_string)
        {
            case "태초의숲":
                currentPage = firstPage;
                break;
            case "비탄의바다":
                currentPage = secondPage;
                break;
            case "타오르는황야":
                currentPage = thirdPage;
                break;
        }
    }

    public void ChangeMap() // 맵이 변경될 때 일지 다시 불러오기
    {
        List<JourneyList> _tempList = new List<JourneyList>(); // 임시 리스트

        for (int i = 0; i < JourneyDataManager.instance._journeyList.Count; i++)
        {
            currentJourney = JourneyDataManager.instance._journeyList[i]._journey;
            GetCurrentScene(JourneyDataManager.instance._journeyList[i]._map);

            for (int j = 0; j < _tempList.Count; j++) // 중복 검사
                if (_tempList[i]._journey.journeyNumber == currentJourney.journeyNumber) // 중복 시 리턴
                    return;

            for (int j = 0; j < _tempList.Count; j++) // 해당 아이템이 이미 있는지 검사
                if (currentJourney.journeyID == _tempList[j]._journey.journeyID)
                {
                    AddJourneyString(j);
                    return;
                }

            _tempList.Add(new JourneyList(currentJourney, currentPageName));

            GameObject journeyObject = Instantiate(JourneySlot, currentPage); // 일지 오브젝트 생성

            journeyObject.name = currentJourney.journeyID; // 일지 오브젝트의 이름은 일지 아이디로 지정

            TextMeshProUGUI journeyName = journeyObject.transform.Find("Journey Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI journeyText = journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>();

            //FIndJourneyByScriptID(ScriptManager.instance.currentScript.scriptID);

            journeyName.text = currentJourney.journeyName;
            journeyText.text = currentJourney.journeyString;

            // 스토리 일지의 경우 분홍색으로 표시
            if (currentJourney.journeyType == "STORY")
                journeyName.color = new Color32(252, 173, 244, 255);

            // 치환
            string replaceText = journeyText.text;

            replaceText = replaceText.Replace("'", ",");
            replaceText = replaceText.Replace("ㅇㅇㅇ", DataManager.instance.nowPlayer.nowCharacterInKor);
            replaceText = replaceText.Replace("\\n", "\n");

            journeyText.text = replaceText;
        }
    }
}
