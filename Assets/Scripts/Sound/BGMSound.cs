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
        // AudioSource의 기본적인 소리크기 설정
        audioSource.volume = 0.2f;

        // 슬라이더 초기 값을 AudioSource의 볼륨과 일치시킵니다.
        if (volumeSlider != null) // 슬라이더가 할당되어 있어야 함을 확인합니다.
        {
            volumeSlider.value = audioSource.volume;
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

        while (audioSource.volume < 0.2f)
        {
            audioSource.volume += 0.3f * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = 0.2f;
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