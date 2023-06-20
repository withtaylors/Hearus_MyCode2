using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerSound : MonoBehaviour
{
    public AudioSource walkSound; // 걷는 소리를 재생하기 위한 AudioSource 컴포넌트 변수
    public AudioSource runSound; // 뛰는 소리를 재생하기 위한 AudioSource 컴포넌트 변수
    //public AudioSource jumpSound; // 뛰는 소리를 재생하기 위한 AudioSource 컴포넌트 변수

    public AudioSource groundedSound; // 땅에 착지하는 소리를 재생하기 위한 AudioSource 컴포넌트 변수
    private bool wasGrounded; // 이전 프레임에서 grounded 상태를 기록하는 변수

    public AudioSource pickSound;
    private bool wasPicking; // 이전 프레임에서 isPicking 상태를 기록하는 변수

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

        //점프 효과음 재생
        /*if (Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
        }*/


        // 착지 효과음 재생/멈춤
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


        //줍는 효과음 재생/멈춤
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
    }

    IEnumerator PlayPickSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickSound.Play();
    }

}
