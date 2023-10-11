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
    private Coroutine fadeOutCoroutine; // 소리가 점점 작아지게끔 하는 코루틴을 저장할 변수

    void Start()
    {
        player = GetComponent<playerController>();
        if (player.grounded)
        {
            groundedSound.Stop();
        }
    }

    void Update()
    {        
        if (player == null)
        {
            return;
        }

        if (player.grounded)
        {
            if(CrossWater.isPlayerOnWater)
            {
                walkSound.Stop();
                runSound.Stop();
            }
            else
            {
                if (player.isWalking && !player.isRunning && !Input.GetKey(KeyCode.LeftShift)) // Shift 키를 누르지 않은 상태에서만 걷는 소리 재생
                {
                    if (!walkSound.isPlaying)
                    {
                        walkSound.Play();
                    }
                }
                else if (!player.isRunning || !Input.GetKey(KeyCode.LeftShift)) 
                {
                    if (walkSound.isPlaying)
                    {
                        walkSound.Stop();
                    }
                }

                if (player.isRunning && Input.GetKey(KeyCode.LeftShift)) //Shift 키를 누른 상태에서 뛰는 소리 재생
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
        else
        {
            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }
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

        if (player.isClimbing) // isClimbing을 기준으로 소리를 재생하거나 정지
        {
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
            {
                if (!climbSound.isPlaying)
                {
                    climbSound.volume = 1; // 사운드를 다시 키면 볼륨을 원래 크기로 재생
                    climbSound.Play();
                }

                if (fadeOutCoroutine != null) // 이미 fadeOut 코루틴이 실행 중이면 중지
                {
                    StopCoroutine(fadeOutCoroutine);
                    fadeOutCoroutine = null;
                }
            }
            else if (climbSound.isPlaying) // 입력이 없을 경우 점차적으로 소리 크기를 줄임
            {
                if (fadeOutCoroutine == null) // fadeOut 코루틴이 실행 중이지 않으면 시작
                {
                    fadeOutCoroutine = StartCoroutine(FadeOutSound(0.3f));
                }
            }
        }
        else if (!player.isClimbing && climbSound.isPlaying)
        {
            if (fadeOutCoroutine == null) // fadeOut 코루틴이 실행 중이지 않으면 시작
            {
                fadeOutCoroutine = StartCoroutine(FadeOutSound(0.3f));
            }
        }
    }

    IEnumerator PlayPickSoundDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        pickSound.Play();
    }

    // 0.5 초 동안 점차적으로 소리를 줄여 주는 코루틴
    IEnumerator FadeOutSound(float duration)
    {
        float initialVolume = climbSound.volume;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            // 볼륨을 점차적으로 줄임
            climbSound.volume = Mathf.Lerp(initialVolume, 0, elapsedTime / duration);
            yield return null;
        }

        climbSound.Stop(); // 소리 완전히 중단
        climbSound.volume = initialVolume; // 볼륨을 원래 값으로 설정
        fadeOutCoroutine = null; // 코루틴 상태를 초기화
    }
}