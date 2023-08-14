using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingTest : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;

    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;

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

        backgroundSlider.value = backgroundFloat;
        soundEffectsSlider.value = soundEffectsFloat;

        backgroundSlider.onValueChanged.AddListener(delegate { UpdateSound(); });
        soundEffectsSlider.onValueChanged.AddListener(delegate { UpdateSound(); });
    }


    // //게임 시작시 불러올 소리 있는지 확인
    // void Start()
    // {
    //     backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
    //     backgroundSlider.value = backgroundFloat;
    //     soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
    //     soundEffectsSlider.value = soundEffectsFloat;
    // }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }

    void onApplicationFocus(bool inFocus)
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

        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
    }
}