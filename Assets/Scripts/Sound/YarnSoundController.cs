using UnityEngine;

public class YarnSoundController : MonoBehaviour
{
    public AudioSource audioSource; // 효과음 재생을 위한 AudioSource
    public AudioClip scriptSound;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void ScriptSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}