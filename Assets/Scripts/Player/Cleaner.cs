using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject respawnParticlesPrefab;
    [SerializeField] private Transform particlesSpawnPoint;
    [SerializeField] private AudioSource audioSource;

    private bool isFadingOut = false;
    private float fadeOutDuration = 3.0f; // 효과음 서서히 줄이기에 걸리는 시간

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = respawnPoint.transform.position;

            if (respawnParticlesPrefab != null)
            {
                GameObject particlesInstance = Instantiate(respawnParticlesPrefab, particlesSpawnPoint.position, Quaternion.identity);
                ParticleSystem particles = particlesInstance.GetComponent<ParticleSystem>();
                if (particles != null)
                {
                    particles.Play();
                    Destroy(particlesInstance, particles.main.duration);
                }
            }

            if (audioSource != null)
            {
                audioSource.Play();
                isFadingOut = true; // 소리 서서히 줄이기 플래그 활성화
            }

            PlayerHP.instance.DecreaseHP(10);
            //DataManager.instance.SaveData(DataManager.instance.nowSlot);
        }
    }

    private void Update()
    {
        if (isFadingOut)
        {
            float elapsed = audioSource.time; // 재생된 시간을 가져옴
            float volume = 1.0f - Mathf.Clamp01(elapsed / fadeOutDuration); // 서서히 줄이는 볼륨 계산
            audioSource.volume = volume;

            if (volume <= 0.0f)
            {
                audioSource.Stop();
                isFadingOut = false;
            }
        }
    }
}
