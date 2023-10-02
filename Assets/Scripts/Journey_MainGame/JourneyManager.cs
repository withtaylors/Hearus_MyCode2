using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    private void Start()
    {
        instance = this;

        journeyManager = FindObjectOfType<JourneyManager>();
        journeyManager.LoadJourney(journeyManager.GetJourney());
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
        for (int i = 0; i <= journey.journeys.Length; i++)
        {
            if (journey.journeys[i].scriptNumber == _scriptID)
            {
                currentJourney = journey.journeys[i - 1];
                break;
            }
        }
    }
}
