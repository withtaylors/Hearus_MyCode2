using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider soundSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("SoundVolume");
            soundSlider.value = savedVolume;
            SoundManager.instance.SetSFXVolume(savedVolume);
            SoundManager.instance.SetMusicVolume(savedVolume);
        }
        else
        {
            soundSlider.value = SoundManager.instance.sfxSource.volume;
        }

        soundSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        SoundManager.instance.SetMusicVolume(volume);
        SoundManager.instance.SetSFXVolume(volume);

        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }
}