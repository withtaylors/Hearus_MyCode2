using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //movement variable
    public float runSpeed;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;
    //bool facingFront;

    //캐릭터 바닥에 위치하는지(점프를 위함)
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    //버튼을 눌렀는지 감지하기위함
    void FixedUpdate()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }
        //바닥과 캐릭터가 터치가 되었을 경우
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        myAnim.SetBool("grounded", grounded);


        float hmove = Input.GetAxis("Horizontal");
        //float vmove = Input.GetAxis("Vertical");

        myAnim.SetFloat("speed", Mathf.Abs(hmove));
        //myAnim.SetFloat("speed", Mathf.Abs(vmove));

        myRB.velocity = new Vector3(0, myRB.velocity.y, -hmove * runSpeed);
        //myRB.velocity = new Vector3(vmove * runSpeed, myRB.velocity.y, -hmove * runSpeed);

        if (hmove > 0 && facingRight) Flip();
        else if (hmove < 0 && !facingRight) Flip();

/*        if (vmove > 0 && facingFront) Flip2();
        else if (vmove < 0 && !facingFront) Flip2();*/
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;

        theScale.z *= -1;
        transform.localScale = theScale;
    }

/*    void Flip2()
    {
        facingFront = !facingFront;
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        transform.localScale = theScale;
    }*/
}
