using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicVolumeSlider.value = savedMusicVolume;
            SoundManager.instance.SetMusicVolume(savedMusicVolume);
        }
        else
        {
            musicVolumeSlider.value = SoundManager.instance.musicSource.volume;
        }

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume");
            soundVolumeSlider.value = savedSoundVolume;
            SoundManager.instance.sfxSource.volume = savedSoundVolume;
        }
        else
        {
            soundVolumeSlider.value = SoundManager.instance.sfxSource.volume;
        }

        soundVolumeSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    public void SetMusicVolume(float volume)
    {
        SoundManager.instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeSoundVolume(float volume)
    {
        SoundManager.instance.sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume);
        PlayerPrefs.Save();
    }
}
