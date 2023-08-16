using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;

    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    void Start()
    {
        // 볼륨 설정을 불러옵니다.
        float backgroundVolume = PlayerPrefs.GetFloat(BackgroundPref);
        float soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsPref);

        // 오디오 소스의 볼륨을 설정합니다.
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundVolume;
        }

        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsVolume;
        }

        // 슬라이더 값 업데이트
        backgroundSlider.value = backgroundVolume;
        soundEffectsSlider.value = soundEffectsVolume;
    }

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundFloat;
        }
        
        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsFloat;
        }

        Debug.Log("CS 배경음볼륨: "+ backgroundSlider.value);
        Debug.Log("CS 효과음볼륨: "+ soundEffectsSlider.value );

        backgroundSlider.value = backgroundFloat;
        soundEffectsSlider.value = soundEffectsFloat;
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);

        Debug.Log("Test SAVE 배경음볼륨: "+ backgroundSlider.value);
        Debug.Log("Test SAVE 효과음볼륨: "+ soundEffectsSlider.value );
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            backgroundAudio[i].volume = backgroundSlider.value;
        }
        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }

        Debug.Log("US 배경음볼륨: "+ backgroundSlider.value);
        Debug.Log("US 효과음볼륨: "+ soundEffectsSlider.value );
    }
}