using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextLogs : MonoBehaviour
{
    public static TextLogs instance;

    public TextMeshProUGUI textPrefab;
    public Transform tf_textPrefab;

    private List<TextMeshProUGUI> logList = new List<TextMeshProUGUI>();

    private string log;

    private void Start()
    {
        instance = this;
    }

    public void GetItemLog(int _itemID) // 아이템 획득 로그
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
        {
            if (ItemDatabase.itemList[i].itemID == _itemID)
            {
                log = (ItemDatabase.itemList[i].itemName + "을/를 획득했습니다");
            }
        }

        CreateLog();
    }

    public void GetJourneyLog() // 일지 업데이트 로그
    {

    }

    private void CreateLog() // 로그 생성
    {
        TextMeshProUGUI textLogs = Instantiate(textPrefab, tf_textPrefab).GetComponent<TextMeshProUGUI>();
        textLogs.text = log;
        logList.Add(textLogs);
        Debug.Log(logList);
        StartCoroutine("FadeOutStart", textLogs);
    }

    private IEnumerator FadeOutStart(TextMeshProUGUI _textLogs) // 로그 페이드아웃 후 파괴
    {
        yield return new WaitForSeconds(3f);

        Color c = _textLogs.color;

        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            c.a = f;
            _textLogs.color = c;
            yield return null;
        }

        Destroy(_textLogs);
    }


}
