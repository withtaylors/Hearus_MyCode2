using UnityEngine;
using System.Collections;
using TMPro;

public class TextBlink : MonoBehaviour
{
    TextMeshProUGUI flashingText;
    bool isBlinking = false; // 깜빡임 상태 플래그

    public Color firstColor = Color.white; // 첫 번째 색상
    public Color secondColor = Color.gray; // 두 번째 색상
    public float blinkInterval = 0.5f; // 깜빡임 간격

    // Use this for initialization
    void Start()
    {
        flashingText = GetComponent<TextMeshProUGUI>();
    }

    // 깜빡임을 시작하는 메서드
    public void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkText());
        }
    }

    // 깜빡임을 멈추는 메서드
    public void StopBlink()
    {
        if (isBlinking)
        {
            isBlinking = false;
            flashingText.color = firstColor;
        }
    }

    IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            flashingText.color = secondColor;
            yield return new WaitForSeconds(blinkInterval);

            flashingText.color = firstColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
