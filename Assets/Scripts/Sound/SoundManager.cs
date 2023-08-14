using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;
    private AudioSource sfxSource;

    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // PlayerPrefs로부터 Slider의 값들을 불러옵니다.
        float musicVolume = PlayerPrefs.GetFloat("musicvolume", 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat("sfxvolume", 0.5f);

        // 불러온 값을 Slider와 AudioSource에 설정합니다.
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicvolume", volume);
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("sfxvolume", volume);
        sfxSource.volume = volume;
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
