using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkipBtn : MonoBehaviour
{
    public Button button;
    public float fadeDuration = 2f; // 페이드 지속 시간
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = button.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // 버튼을 처음에 숨김
        button.interactable = false; // 버튼 비활성화

        // 4초 뒤에 페이드 인 코루틴을 시작
        StartCoroutine(StartFadeAfterDelay(3f));
    }

    private void Update()
    {
        // 엔터 키를 눌렀을 때 skip 효과
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnButtonClick();
        }
    }

    private IEnumerator StartFadeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 4초 뒤에 페이드 인 코루틴을 시작
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        float startAlpha = canvasGroup.alpha;
        float endAlpha = 1; // 최종 투명도

        float startTime = Time.time;
        float endTime = startTime + fadeDuration;

        while (Time.time < endTime)
        {
            float timeRatio = (Time.time - startTime) / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timeRatio);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        button.interactable = true; // 버튼 활성화
    }

    public void OnButtonClick()
    {
        Debug.Log("Clicked!");
        //게임씬이동
        ChangeScene.target();
    }
}
