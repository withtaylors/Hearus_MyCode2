using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{
    public Option[] OptionParse(string _OptionCSVFileName)
    {
        List<Option> optionList = new List<Option>();
        TextAsset csvData = Resources.Load<TextAsset>(_OptionCSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Option option = new Option();

            string optionID;
            List<string> isExistNextScript = new List<string>();
            List<string> nextScriptNumber = new List<string>();
            List<string> sentenceList = new List<string>();

            optionID = row[0];

            do
            {                
                sentenceList.Add(row[1]);
                isExistNextScript.Add(row[2]);
                nextScriptNumber.Add(row[3]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                    break;
            } while (row[0].ToString() == "");

            option.optionID = optionID;
            option.sentences = sentenceList.ToArray();
            option.isExistNextScript = isExistNextScript.ToArray();
            option.nextScriptNumber = nextScriptNumber.ToArray();

            optionList.Add(option);
        }

        return optionList.ToArray();
    }

    public Script[] Parse(string _ScriptCSVFileName)
    {
        List<Script> scriptList = new List<Script>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_ScriptCSVFileName); // CSV 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터 단위로 쪼개기
        
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // 각 열에서 콤마 단위로 쪼개기

            Script script = new Script(); // 대사 리스트 생성

            string scriptID;
            string itemID;
            string isExistOption;
            string optionNumber;
            string isExistNextScript;
            string nextScriptNumber;
            string eventName;
            string scriptSwitch;
            List<string> sentenceList = new List<string>();

            scriptID = row[0];
            itemID = row[1];
            isExistOption = row[3];
            optionNumber = row[4];
            isExistNextScript = row[5];
            nextScriptNumber = row[6];
            eventName = row[7];
            scriptSwitch = row[8];

            do
            {
                sentenceList.Add(row[2]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                } else
                    break;
            } while (row[0].ToString() == "");

            script.itemID = itemID;
            script.scriptID = scriptID;
            script.isExistOption = isExistOption;
            script.optionNumber = optionNumber;
            script.isExistNextScript = isExistNextScript;
            script.nextScriptNumber = nextScriptNumber;
            script.sentences = sentenceList.ToArray();
            script.eventName = eventName;
            script.scriptSwitch = scriptSwitch;


            scriptList.Add(script);
        }

        return scriptList.ToArray();
    }

    private void Start()
    {
        Parse("ItemScript");
        OptionParse("OptionList");
    }
}
