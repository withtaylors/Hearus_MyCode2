using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyParser : MonoBehaviour
{
    public Journey[] JourneyParse(string _JourneyCSVFileName)
    {
        List<Journey> journeyList = new List<Journey>(); // 일지 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(_JourneyCSVFileName); // CSV 파일 가져오기

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터 단위 분할

        for (int i = 0; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // 각 열에서 콤마 단위 분할

            Journey journey = new Journey(); // 일지 생성

            string journeyNumber;
            string scriptNumber;
            string itemNumber;
            string journeyType;
            List<string> journeyString = new List<string>();

            journeyNumber = row[0];
            scriptNumber = row[1];
            itemNumber = row[2];
            journeyType = row[4];

            do
            {
                journeyString.Add(row[3]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else break;
            } while (row[0].ToString() == ""); // 일지 번호가 갱신될 때까지 반복

            journey.journeyNumber = journeyNumber;
            journey.scriptNumber = scriptNumber;
            journey.itemNumber = itemNumber;
            journey.journeyType = journeyType;
            journey.journeyString = journeyString.ToArray();

            journeyList.Add(journey);
        }

        return journeyList.ToArray();
    }

    private void Start()
    {
        JourneyParse("Journey");
    }
}
