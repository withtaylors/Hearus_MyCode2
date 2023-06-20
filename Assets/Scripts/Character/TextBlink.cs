using UnityEngine;
using System.Collections;
using TMPro;

public class TextBlink : MonoBehaviour
{
    TextMeshProUGUI flashingText;

    public Color startColor = Color.white; // 시작 색상 (흰색)
    public Color endColor = Color.gray; // 종료 색상 (회색)
    public float transitionDuration = 0.5f; // 색상 변화에 걸리는 시간

    // Use this for initialization
    void Start()
    {
        flashingText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                flashingText.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            flashingText.color = endColor;

            yield return new WaitForSeconds(0.5f); // 텍스트가 회색인 상태를 유지하는 시간

            elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                flashingText.color = Color.Lerp(endColor, startColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            flashingText.color = startColor;

            yield return new WaitForSeconds(0.5f); // 텍스트가 흰색인 상태를 유지하는 시간
        }
    }
}
