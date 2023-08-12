using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Option
{
    [Tooltip("옵션 아이디")]
    public string optionID;

    [Tooltip("다음 스크립트 존재 여부")]
    public string[] isExistNextScript;

    [Tooltip("다음 스크립트 번호")]
    public string[] nextScriptNumber;

    [Tooltip("대사 내용")]
    public string[] sentences;
}

[System.Serializable]
public class OptionEvent
{
    public Vector2 line;
    public Option[] options;
}
