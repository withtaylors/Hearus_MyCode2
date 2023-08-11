using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 추가

public class AudioManagerTutorial : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    private int firstPlayInt;
    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;

    public AudioSource[] backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    private int currentArea = -1;  // 추가

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            backgroundFloat = .125f;
            soundEffectsFloat = .75f;

            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;
        }
    }

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
    }

    // 추가
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            int debug_previousArea = currentArea;
            currentArea = other.GetComponent<AreaTrigger>().areaIndex;

            if (debug_previousArea != currentArea)
            {
                Debug.Log($"Area changed from {debug_previousArea} to {currentArea}");
            }

            ChangeBackgroundMusic(currentArea);
        }
    }

    // 추가
    public void ChangeBackgroundMusic(int index)
    {
        for (int i = 0; i < backgroundAudio.Length; i++)
        {
            if (i == index)
            {
                backgroundAudio[i].Play();
            }
            else
            {
                backgroundAudio[i].Stop();
            }
        }
    }
}
