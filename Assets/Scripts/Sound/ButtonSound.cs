using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound; // 마우스를 올렸을 때 재생될 효과음
    public AudioClip clickSound; // 버튼을 클릭했을 때 재생될 효과음

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}