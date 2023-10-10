using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    [Tooltip("스크립트 아이디")]
    public string scriptID;

    [Tooltip("아이템 아이디")]
    public string itemID;

    [Tooltip("선택지 존재 여부")]
    public string isExistOption;

    [Tooltip("선택지 번호")]
    public string optionNumber;

    [Tooltip("다음 스크립트 존재 여부")]
    public string isExistNextScript;

    [Tooltip("다음 스크립트 번호")]
    public string nextScriptNumber;

    [Tooltip("이벤트 이름")]
    public string eventName;

    [Tooltip("조건 충족 여부")]
    public string scriptSwitch;

    [Tooltip("획득 시점 여부")]
    public string getTiming;

    [Tooltip("일지 번호")]
    public string journeyNumber;

    [Tooltip("아이템 설명")]
    public string itemDes;

    [Tooltip("대사 내용")]
    public string[] sentences;
}

[System.Serializable]
public class ScriptEvent
{
    public Vector2 line; // 몇 번째 대사를 추출할지
    public Script[] scripts;
}
