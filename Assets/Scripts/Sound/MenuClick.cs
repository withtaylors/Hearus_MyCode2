using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuClick : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip clickSound; // 버튼을 클릭했을 때 재생될 효과음

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
