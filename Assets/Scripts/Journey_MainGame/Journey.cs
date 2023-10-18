using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Journey
{

    [Tooltip("일지 순서")]
    public string journeyNumber;

    [Tooltip("일지 아이디")]
    public string journeyID;

    [Tooltip("일지 이름")]
    public string journeyName;

    [Tooltip("스크립트 번호")]
    public string scriptNumber;

    [Tooltip("아이템 번호")]
    public string itemNumber;

    [Tooltip("일지 내용")]
    public string journeyString;

    [Tooltip("구분")]
    public string journeyType;
}

[System.Serializable]
public class JourneyEvent 
{
    public Vector2 line;
    public Journey[] journeys;
}

