using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //게임 시작시 나오는 소리
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    private int firstPlayInt;
    public Slider backgroundSlider, soundEffectsSlider;
    private float backgroundFloat, soundEffectsFloat;

    public AudioSource backgroundAudio;
    public AudioSource[] soundEffectsAudio;

    //게임 시작시 불러올 소리 있는지 확인
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        //처음 시작
        if(firstPlayInt == 0)
        {
            //DEFAULT VOLUME 설정
            backgroundFloat = .5f;
            soundEffectsFloat = .75f;

            //background/soundeffectfloat이랑 slider value 일치시키기
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);

            Debug.Log("FP 배경음볼륨: "+ backgroundFloat);
            Debug.Log("FP 효과음볼륨: "+ soundEffectsFloat );

            Debug.Log("FP 배경음볼륨: "+ backgroundFloat);
            Debug.Log("FP 효과음볼륨: "+ soundEffectsSlider.value );
        }
        //시작 경험 있음
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            soundEffectsSlider.value = soundEffectsFloat;

            Debug.Log("NFP 배경음볼륨: "+ backgroundFloat);
            Debug.Log("NFP 효과음볼륨: "+ soundEffectsFloat );

            Debug.Log("NFP 배경음볼륨2: "+ backgroundSlider.value);
            Debug.Log("NFP 효과음볼륨2: "+ soundEffectsSlider.value );
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);

        Debug.Log("SAVE 배경음볼륨: "+ backgroundSlider.value);
        Debug.Log("SAVE 효과음볼륨: "+ soundEffectsSlider.value );
    }

    void onApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    //음량조절시마다 불림
    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundSlider.value;

        for(int i=0; i<soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsSlider.value;
        }

        Debug.Log("AM US 배경음볼륨: "+ backgroundSlider.value);
        Debug.Log("AM US 효과음볼륨: "+ soundEffectsSlider.value );
    }
}