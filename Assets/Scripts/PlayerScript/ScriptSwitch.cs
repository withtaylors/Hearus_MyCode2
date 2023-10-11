using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Switch
{
    public string switchName;
    public bool switchValue;

    public Switch(string _switchName, bool _switchValue) // 생성자
    {
        switchName = _switchName;
        switchValue = _switchValue;
    }
}

public class ScriptSwitch : MonoBehaviour
{
    public bool hasPassedTime_113; // 송이버섯 습득 후 시간 경과 여부

    public List<Switch> switchs;

    public static ScriptSwitch instance;

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
        switchs = new List<Switch>();

        switchs.Add(new Switch("noticesHerb_122", false));
        switchs.Add(new Switch("noticesHerb_123", false));
        switchs.Add(new Switch("noticesHerb_124", false));
    }
}
