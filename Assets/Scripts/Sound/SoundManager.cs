using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] musicClips; // 배경음 배열
    public AudioClip[] sfxClips; // 효과음 배열

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlayMusic(int musicIndex)
    {
        if (musicIndex < musicClips.Length && musicIndex >= 0)
        {
            musicSource.clip = musicClips[musicIndex];
            musicSource.Play();
        }
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < sfxClips.Length && sfxIndex >= 0)
        {
            sfxSource.PlayOneShot(sfxClips[sfxIndex]);
        }
    }
}
