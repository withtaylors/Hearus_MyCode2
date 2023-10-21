using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskFactors : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            PlayerHP.instance.DecreaseHP(10);
            Debug.Log("DecreaseHP");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            PlayerHP.instance.DecreaseHP(10);
            Debug.Log("DecreaseHP");
        }
    }
}
