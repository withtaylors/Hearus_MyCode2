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

    [Tooltip("대사 내용")]
    public string[] sentences;
}

[System.Serializable]
public class ScriptEvent
{
    public string scriptID;
    public string itemID;

    public Vector2 line; // 몇 번째 대사를 추출할지
    public Script[] scripts;
}
