using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
        public Rigidbody myRB;
    public Animator myAnim;

    public bool grounded=false;

        void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }


void OnTriggerEnter(Collider other)
{
    if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
    {
        grounded = true;
        myAnim.SetBool("grounded", grounded);
    }
}

void OnTriggerExit(Collider other)
{
    if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
    {
        grounded = false;
        myAnim.SetBool("grounded", grounded);
                myAnim.SetBool("falling to land", true);
    }
}

}
