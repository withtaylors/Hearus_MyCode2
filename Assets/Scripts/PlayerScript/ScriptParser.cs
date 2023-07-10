using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptParser : MonoBehaviour
{
    public Script[] Parse(string _CSVFileName)
    {
        List<Script> scriptList = new List<Script>(); // 대사 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // CSV 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터 단위로 쪼개기
        
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // 각 열에서 콤마 단위로 쪼개기

            Script script = new Script(); // 대사 리스트 생성

            List<string> sentenceList = new List<string>();

            do
            {
                sentenceList.Add(row[2]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                } else
                    break;
            } while (row[0].ToString() == "");

            script.sentences = sentenceList.ToArray();

            scriptList.Add(script);
        }

        return scriptList.ToArray();
    }

    private void Start()
    {
        Parse("ItemScript");
    }
}
