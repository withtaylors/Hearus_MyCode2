using UnityEngine;
using UnityEngine.UI;

public class CanvasToggle : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvas2;

    public float delayDuration = 2f;

    private void Start()
    {
        canvas.SetActive(false); // 시작 시 Canvas를 비활성화합니다.
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true); // Canvas를 활성화합니다.
        Invoke("HideCanvas", delayDuration); // 지정한 시간(delayDuration) 후에 HideCanvas 메서드를 호출합니다.
    }

    public void HideCanvas()
    {
        canvas.SetActive(false); // Canvas를 비활성화합니다.
        canvas2.SetActive(false); // Canvas를 비활성화합니다.
    }
}