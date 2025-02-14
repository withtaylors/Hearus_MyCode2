using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] string csv_ScriptFileName;
    [SerializeField] string csv_OptionFileName;

    Dictionary<int, Script> scriptDic = new Dictionary<int, Script>();
    Dictionary<int, Option> optionDic = new Dictionary<int, Option>();

    public static bool isFinish = false; // 파싱된 데이터가 전부 저장되었는지

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            ScriptParser theParser = GetComponent<ScriptParser>();

            Script[] scripts = theParser.Parse(csv_ScriptFileName); // scripts에 파싱된 스크립트들 저장
            for (int i = 0; i < scripts.Length; i++)
            {
                scriptDic.Add(i + 1, scripts[i]);
            }

            Option[] options = theParser.OptionParse(csv_OptionFileName);
            for (int i = 0; i < options.Length; i++)
            {
                optionDic.Add(i + 1, options[i]);
            }

            isFinish = true;
        }
    }

    public Script[] GetScript(int _StartLine, int _EndLine)
    {
        List<Script> scriptList = new List<Script>();

        for (int i = 0; i <= _EndLine - _StartLine; i++)
        {
            scriptList.Add(scriptDic[_StartLine + i]);
        }

        return scriptList.ToArray();
    }

    public Option[] GetOption(int _StartLine, int _EndLine)
    {
        List<Option> optionList = new List<Option>();

        for (int i = 0; i <= _EndLine - _StartLine; i++)
        {
            optionList.Add(optionDic[_StartLine + i]);
        }

        return optionList.ToArray();
    }
}
