using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 호버 음향 효과를 실행합니다.
        if(hoverSound != null)
            hoverSound.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭 음향 효과를 실행합니다.
        if(clickSound != null)
            clickSound.Play();
    }
}
