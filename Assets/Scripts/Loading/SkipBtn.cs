using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkipBtn : MonoBehaviour
{
    public float fadeInDuration = 2.0f; // 나타나는 데 걸리는 시간(초)
    private Image buttonImage;
    private float targetAlpha = 1.0f; // 목표 알파 값 (1: 완전 불투명)

    private void Start()
    {
        buttonImage = GetComponent<Image>(); // 버튼의 Image 컴포넌트 가져오기

        // 버튼 알파 값을 초기화하고 비활성화
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0);
        gameObject.SetActive(false);

        // 일정 시간 후에 버튼을 나타나게 함
        StartCoroutine(EnableButtonWithFadeIn());
    }

    private IEnumerator EnableButtonWithFadeIn()
    {
        yield return new WaitForSeconds(fadeInDuration);

        float elapsedTime = 0;

        // 알파 값을 서서히 증가시키는 애니메이션
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, targetAlpha, elapsedTime / fadeInDuration);
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, newAlpha);
            yield return null;
        }

        // 애니메이션이 완료되면 버튼을 활성화
        gameObject.SetActive(true);
    }

    public void OnButtonClick()
    {

        Debug.Log("Clicked!");
    }
}
