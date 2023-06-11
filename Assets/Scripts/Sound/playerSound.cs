using UnityEngine;
using UnityEngine.UI;

public class playerSound : MonoBehaviour
{
    public AudioSource walkSound; // 걷는 소리를 재생하기 위한 AudioSource 컴포넌트 변수
    public AudioSource runSound; // 뛰는 소리를 재생하기 위한 AudioSource 컴포넌트 변수

    private playerController player; // Reference to the playerController script

    void Start()
    {
        player = GetComponent<playerController>(); // Get the playerController component from the same GameObject
    }

    void Update()
    {
        // Check if player reference is valid
        if (player == null)
        {
            Debug.LogWarning("Player reference is null. Make sure the playerController script is attached to the same GameObject.");
            return;
        }

        // 걷는 효과음 재생/정지
        if (player.isWalking && !player.isRunning)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
        else
        {
            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }
        }

        // 뛰는 효과음 재생/정지
        if (player.isRunning)
        {
            if (!runSound.isPlaying)
            {
                runSound.Play();
            }
        }
        else
        {
            if (runSound.isPlaying)
            {
                runSound.Stop();
            }
        }
    }
}
