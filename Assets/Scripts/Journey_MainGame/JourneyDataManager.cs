using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyDataManager : MonoBehaviour
{
    public static JourneyDataManager instance;

    [SerializeField] string csv_JourneyFileName;

    Dictionary<int, Journey> journeyDic = new Dictionary<int, Journey>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            JourneyParser theJourneyParser = GetComponent<JourneyParser>();

            Journey[] journeys = theJourneyParser.JourneyParse(csv_JourneyFileName);
            for (int i = 0; i < journeys.Length; i++)
            {
                journeyDic.Add(i + 1, journeys[i]);
            }
        }
    }

    public Journey[] GetJourney(int _StartLine, int _EndLine)
    {
        List<Journey> journeyList = new List<Journey>();

        for (int i = 0; i <= _EndLine - _StartLine; i++)
        {
            journeyList.Add(journeyDic[_StartLine + i]);
        }

        return journeyList.ToArray();
    }
}
