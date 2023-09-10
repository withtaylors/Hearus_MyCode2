using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSound : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    public float fadeInTime = 1.0f; // 볼륨 증가에 걸리는 시간, 초단위
    public float fadeOutTime = 1.5f; // 볼륨 감소에 걸리는 시간, 초단위

    void Awake()
    {
        if (volumeSlider != null) // 슬라이더가 할당되어 있어야 함을 확인
        {
            // AudioSource의 볼륨을 슬라이더 값에 맞게 설정
            audioSource.volume = volumeSlider.value;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
            StartCoroutine(FadeIn(audioSource, fadeInTime));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && audioSource.isPlaying)
        {
            StartCoroutine(FadeOut(audioSource, fadeOutTime));
        }
    }

    public void OnVolumeChanged(float value)
    {
        // 슬라이더가 할당되어 있으면 볼륨을 설정
        if (volumeSlider != null)
        {
            audioSource.volume = value;
        }
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;

        while (audioSource.volume < volumeSlider.value) // 0.2f를 슬라이더 값으로 변경
        {
            audioSource.volume += 0.3f * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = volumeSlider.value; // 0.2f를 슬라이더 값으로 변경
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}