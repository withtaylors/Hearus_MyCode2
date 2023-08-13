using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerSound : MonoBehaviour
{
    public AudioSource walkSound;
    public AudioSource runSound;
    public AudioSource groundedSound;
    private bool wasGrounded;

    public AudioSource pickSound;
    private bool wasPicking;

    public AudioSource climbSound;

    private playerController player;

    void Start()
    {
        player = GetComponent<playerController>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is null. Make sure the playerController script is attached to the same GameObject.");
            return;
        }

        if (player.isWalking && !player.isRunning && !Input.GetKey(KeyCode.LeftShift)) // Shift 키를 누르지 않은 상태에서만 걷는 소리 재생
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
            else if (!player.isRunning || !Input.GetKey(KeyCode.LeftShift)) // 수정된 부분
        {
            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }
        }

            if (player.isRunning && Input.GetKey(KeyCode.LeftShift)) // 수정된 부분: Shift 키를 누른 상태에서 뛰는 소리 재생
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

        if (player.grounded && !wasGrounded)
        {
            if (!groundedSound.isPlaying)
            {
                groundedSound.Play();
            }
        }
        else if (!player.grounded && wasGrounded)
        {
            if (groundedSound.isPlaying)
            {
                groundedSound.Stop();
            }
        }
        wasGrounded = player.grounded;

        if (player.isPicking && !wasPicking)
        {
            if (!pickSound.isPlaying)
            {
                StartCoroutine(PlayPickSoundDelayed(1.5f));
            }
        }
        else if (!player.isPicking && wasPicking)
        {
            if (pickSound.isPlaying)
            {
                pickSound.Stop();
            }
        }
        wasPicking = player.isPicking;

         // 밧줄 타기 소리를 재생하거나 정지합니다.
        if (player.isClimbing)
        {
            if (!climbSound.isPlaying)
            {
                climbSound.Play();
            }
        }
        else
        {
            if (climbSound.isPlaying)
            {
                climbSound.Stop();
            }
        }
    }

    IEnumerator PlayPickSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickSound.Play();
    }
}
