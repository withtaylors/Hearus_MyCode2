using UnityEngine;

public class MapSound : MonoBehaviour
{
    private string theCollider;

    private void OnTriggerEnter(Collider other)
    {
        theCollider = other.tag;
        if (theCollider == "Player")
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        theCollider = other.tag;
        if (theCollider == "Player")
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().loop = true;
        }
    }
}
