using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            string journeyID;
            string journeyName;
            string scriptNumber;
            string itemNumber;
            string journeyType;
            string journeyString;

            journeyNumber = row[0];
            journeyID = row[1];
            journeyName = row[2];
            scriptNumber = row[3];
            itemNumber = row[4];
            journeyString = row[5];
            journeyType = row[6];

            do
            {
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' }); // 콤마 단위 분할
                }
                else break;
            } while (row[0].ToString() == ""); // 일지 번호가 갱신될 때까지 반복

            journey.journeyNumber = journeyNumber;
            journey.journeyID = journeyID;
            journey.journeyName = journeyName;
            journey.scriptNumber = scriptNumber;
            journey.itemNumber = itemNumber;
            journey.journeyType = journeyType;
            journey.journeyString = journeyString;

            // 공백 제거
            journey.journeyNumber = string.Concat(journey.journeyNumber.Where(x => !char.IsWhiteSpace(x)));
            journey.journeyID = string.Concat(journey.journeyID.Where(x => !char.IsWhiteSpace(x)));
            journey.scriptNumber = string.Concat(journey.scriptNumber.Where(x => !char.IsWhiteSpace(x)));
            journey.itemNumber = string.Concat(journey.itemNumber.Where(x => !char.IsWhiteSpace(x)));
            journey.journeyType = string.Concat(journey.journeyType.Where(x => !char.IsWhiteSpace(x)));

            // 파싱된 일지 리스트에 추가
            journeyList.Add(journey);
        }

        return journeyList.ToArray();
    }

    private void Start()
    {
        JourneyParse("Journey");
    }
}
