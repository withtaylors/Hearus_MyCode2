using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextLogs : MonoBehaviour
{
    public static TextLogs instance;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private Transform tf_logPrefab;

    //private List<GameObject> logList = new List<GameObject>();
          
    private string log;

    private void Start()
    {
        instance = this;
    }

    public void GetItemLog(int _itemID) // 아이템 획득 로그
    {
        for (int i = 0; i < ItemDatabase.itemList.Count; i++)
            if (ItemDatabase.itemList[i].itemID == _itemID)
            {
                log = (ItemDatabase.itemList[i].itemName + "을/를 획득했습니다");
                break;
            }

        CreateLog(log);
    }

    public void GetTextLog(string _log)
    {
        log = _log;
    }

    public void UpdateJourneyLog() // 일지 업데이트 로그
    {

    }

    private void CreateLog(string _log) // 로그 생성
    {
        GameObject log = Instantiate(logPrefab, tf_logPrefab);
        TextMeshProUGUI logText = log.GetComponentInChildren<TextMeshProUGUI>();
        logText.text = _log;
        log.SetActive(true);
        logText.color = Color.white;
        StartCoroutine(FadeOutStart(log, logText));
    }

    private IEnumerator FadeOutStart(GameObject _textLogs, TextMeshProUGUI _text) // 로그 페이드아웃 후 파괴
    {
        yield return new WaitForSecondsRealtime(3f); // 3초 뒤 파괴되도록 함

        Color32 c1 = _textLogs.GetComponent<Image>().color; // 페이드아웃을 위해 로그의 컬러값을 받아 옴
        Color32 c2 = _text.color;

        for (float f = 1f; f >= 0f; f -= 0.05f) // 페이드아웃
        {
            c1.a = (byte)(f * 40); 
            c2.a = (byte)(f * 255);
            _textLogs.GetComponent<Image>().color = c1;
            _text.color = c2;
            yield return null;
        }

        Destroy(_textLogs); // 페이드아웃이 끝나면 로그가 파괴됨
    }
}