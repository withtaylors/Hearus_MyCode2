using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JourneyList
{
    public Journey _journey;
    public string _map;

    public JourneyList(Journey journey, string map)
    {
        _journey = journey;
        _map = map;
    }
}

public class JourneyDataManager : MonoBehaviour
{
    public static JourneyDataManager instance;

    [SerializeField] string csv_JourneyFileName;

    Dictionary<int, Journey> journeyDic = new Dictionary<int, Journey>();

    public List<JourneyList> _journeyList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            JourneyParser theJourneyParser = GetComponent<JourneyParser>();

            Journey[] journeys = theJourneyParser.JourneyParse(csv_JourneyFileName);

            for (int i = 0; i < journeys.Length; i++)
            {
                journeyDic.Add(i + 1, journeys[i]);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _journeyList = new List<JourneyList>();
    }

    public Journey[] GetJourney(int _StartLine, int _EndLine)
    {
        List<Journey> journeyList = new List<Journey>();

        for (int i = 0; i <= _EndLine - _StartLine; i++)
        {
            journeyList.Add(journeyDic[_StartLine + i + 1]);
        }

        return journeyList.ToArray();
    }
}
