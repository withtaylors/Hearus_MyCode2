using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSwitch : MonoBehaviour
{
    public bool hasPassedTime_113; // 송이버섯 습득 후 시간 경과 여부
    public bool noticesHerb_122; // 까마중 줄기 약초 인지 여부
    public bool noticesHerb_123; // 골쇄보 약초 인지 여부
    public bool noticesHerb_124; // 쐐기풀 약초 인지 여부

    public static ScriptSwitch instance;

    private void Start()
    {
        instance = this;
    }
}
