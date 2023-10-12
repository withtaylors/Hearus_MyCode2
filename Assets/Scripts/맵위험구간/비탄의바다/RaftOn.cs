using System.Collections;
using UnityEngine;

public class RaftOn : MonoBehaviour
{
    public GameObject raft;
    [SerializeField] public Transform respawnPosition;
    public ParticleSystem particleEffect; 
    public Transform particlePosition; 
    public AudioSource audioSource;

    private void Start()
    {
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
            if(raft.activeSelf == false)
            {
                Debug.Log("ObjectAppearOnCollision1 Player OnTriggerEnter 2222222");
                raft.SetActive(true);

                PlayEffects();
                ResetPosition();  
            }
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

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Debug.Log("RaftOn OnTriggerExit");

    //         isPlayerInside = false;
    //     }
    // }
}