using System.Collections;
using UnityEngine;

public class ObjectAppearOnCollision1 : MonoBehaviour
{
    public GameObject raft;
    [SerializeField] public Transform respawnPosition;
    public ParticleSystem particleEffect; 
    public Transform particlePosition; 
    public AudioSource audioSource;
    public bool isFadingIn = false;
    public float fadeSpeed = 0.5f;
    public bool isPlayerInside = false;

    private void Start()
    {
        raft.SetActive(false);
        if (particleEffect != null)
        {
            particleEffect.Stop();
            particleEffect.gameObject.SetActive(false);
        }    
    }

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
            isFadingIn = !isFadingIn;

            StartCoroutine(FadeObject(isFadingIn));

            if (!isFadingIn && raft.activeSelf)
            {
                                    Debug.Log("ObjectAppearOnCollision1 Player OnTriggerEnter 111111");
                        raft.SetActive(true);

                PlayEffects();
                ResetPosition();
            }

            if(isFadingIn && !raft.activeSelf)
            {
                                                    Debug.Log("ObjectAppearOnCollision1 Player OnTriggerEnter 2222222");

                        raft.SetActive(true);

                              PlayEffects();
                ResetPosition();  
            }
            isPlayerInside = true;
         }
     }

     IEnumerator FadeObject(bool fadeIn)
     {
        float startAlpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float timeCounter=0;

         while (timeCounter < fadeSpeed)
         {
             timeCounter += Time.deltaTime;
             float alphaValue=Mathf.Lerp(startAlpha,endAlpha,timeCounter/fadeSpeed);
           yield return null; 
       }      
   }

    void PlayEffects()
    {
                Debug.Log("ObjectAppearOnCollision1 PlayEffects");

        if(particleEffect != null) 
        {
            particleEffect.gameObject.SetActive(true); 
            particleEffect.transform.position = particlePosition.position;
            particleEffect.Play(); 

            StartCoroutine(StopParticleAfterTime(particleEffect.main.duration));
        }

        if(audioSource != null) 
            audioSource.Play(); 
    }

    IEnumerator StopParticleAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (particleEffect != null)
        {
            particleEffect.Stop();
            particleEffect.gameObject.SetActive(false);
        }
    }

   void ResetPosition() 
   {  
            Debug.Log("ObjectAppearOnCollision1   ResetPosition");

        raft.transform.position = respawnPosition.transform.position; 
   }
   
//    void DeactivateRaftIfOutsideTrigger(Transform playerTransform)
//     {  
//         Vector3 directionFromPlayerToTrigger = (transform.position - playerTransform.position).normalized; 
//         bool isOnRightSideOfTrigger = (Vector3.Dot(directionFromPlayerToTrigger, transform.right) > 0); 

//         if (!isPlayerInside && isOnRightSideOfTrigger) 
//         {     
//             ResetPosition();    
//         }       
//     }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("111 OnTriggerExit");

            isPlayerInside = false;
        }
    }
}