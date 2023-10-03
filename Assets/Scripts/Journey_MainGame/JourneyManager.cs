using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JourneyManager : MonoBehaviour
{
    public static JourneyManager instance;

    private JourneyManager journeyManager;

    [SerializeField] private JourneyEvent journey;

    private Journey[] journeys;
    public Journey currentJourney;

    public List<Journey> journeyList;

    public Transform firstPage;
    public Transform secondPage;
    public Transform thirdPage;
    public Transform fourthPage;
    public Transform fifthPage;

    public Transform currentPage;

    public GameObject JourneySlot;

    private void Start()
    {
        instance = this;

        journeyManager = FindObjectOfType<JourneyManager>();
        journeyManager.LoadJourney(journeyManager.GetJourney());

        journeyList = new List<Journey>();
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
            if (int.Parse(journey.journeys[i].journeyNumber).Equals(int.Parse(_journeyNumber)))
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
        GetCurrentScene();

        GameObject journeyObject = Instantiate(JourneySlot, currentPage);

        //FIndJourneyByScriptID(ScriptManager.instance.currentScript.scriptID);

        journeyObject.transform.Find("Journey Name").GetComponent<TextMeshProUGUI>().text = currentJourney.journeyName;
        journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>().text = currentJourney.journeyString[0];

        if (currentJourney.journeyString.Length > 1)
        {
            for (int i = 1; i < currentJourney.journeyString.Length; i++)
                journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>().text =
                    journeyObject.transform.Find("Journey Text").GetComponent<TextMeshProUGUI>().text + "\n" + currentJourney.journeyString[i];
        }
        else
            return;
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
