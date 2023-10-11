using System.Collections;
using UnityEngine;

public class ObjectAppearOnCollisionLeft : MonoBehaviour
{
    public GameObject raft;
    private void Start()
    {
        raft.SetActive(false); 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpecialArea"))
        {
            Debug.Log("ObjectAppearOnCollisionLeft SpecialArea OnTriggerExit");
            raft.SetActive(false);
        }
    }
}