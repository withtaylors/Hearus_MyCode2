using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hoverSound;
            audioSource.volume = SoundManager.instance.sfxSource.volume; // 이 줄이 수정되었습니다.
            audioSource.enabled = true;
            audioSource.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = clickSound;
        audioSource.volume = SoundManager.instance.sfxSource.volume; // 이 줄이 수정되었습니다.
        audioSource.enabled = true;
        audioSource.Play();
    }
}
