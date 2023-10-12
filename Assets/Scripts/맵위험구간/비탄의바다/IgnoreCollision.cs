using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
void Start()
{
    Collider myCollider = GetComponent<Collider>(); // Get the collider attached to this object
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find the object with the "Player" tag

    if (playerObject != null)
    {
        Collider playerCollider = playerObject.GetComponent<Collider>(); // Get the collider attached to the Player object

        if (playerCollider != null)
        {
            Physics.IgnoreCollision(myCollider, playerCollider); // Ignore collision between this object and the Player
        }
    }
}

}
