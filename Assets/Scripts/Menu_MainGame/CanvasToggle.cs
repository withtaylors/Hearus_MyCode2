using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasToggle : MonoBehaviour
{
    public GameObject canvas2;

    public void ShowCanvas()
    {
        canvas2.SetActive(false); // Canvas2를 활성화합니다.
    }
}
