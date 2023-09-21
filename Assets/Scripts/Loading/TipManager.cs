using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TipManager : MonoBehaviour
{
    public TMP_Text tipText;

    private List<string> tips = new List<string>
    {
        "친구는 소중히 여겨야죠! 좋은 일이 생길지도 모르니 친구에게 선물을 주는 건 어떨까요?",
        "잘 모르는 것은 조심 또 조심! 아무거나 주워 먹었다가는 배탈이 날 수도 있어요!",
        "꼼꼼한 조사는 탐사자의 덕목 아니겠어요?",
        "겉모습으로 판단하는 건 좋지 않아요. 쓸모없어 보이는 것도 언젠가는 쓸 일이 생기기 마련이랍니다.",
        "언제나 건강이 최우선! 잊지 마세요.",
        "아무리 외로워도 당신 곁에는 친구가 있다는 사실을 잊지 말아요.",
        "가끔은 살아남기 위해서 도전이 필요한 법이죠.",
        "희망은 가장 큰 무기랍니다. 절대 희망을 잃지 마세요!",
        "세상에 거저 얻는 것은 없답니다. 뭐든 노력해 보세요! 좋은 일이 생길 수도 있으니까요.",
        "맨손으로 살아남기란 정말 힘든 일이죠. 뭐라도 도구를 만들어 보는 건 어떨까요?"
    };

    public float tipChangeInterval = 3f;
    private int currentTipIndex = 0;
    private float timer = 0f;

    private void Start()
    {
        currentTipIndex = Random.Range(0, tips.Count); // 랜덤한 팁 인덱스 선택
        ShowTip(currentTipIndex);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tipChangeInterval)
        {
            timer = 0f;
            currentTipIndex = (currentTipIndex + 1) % tips.Count;
            ShowTip(currentTipIndex);
        }
    }

    private void ShowTip(int index)
    {
        tipText.text = tips[index];
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        float blinkDuration = 1f; // 깜빡임 주기
        float startTime = Time.time;

        while (Time.time - startTime < blinkDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, Mathf.PingPong((Time.time - startTime) / blinkDuration, 1f));
            tipText.alpha = alpha;
            yield return null;
        }
    }
}
