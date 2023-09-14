using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FrithControllerTest4 : MonoBehaviour {

    public Transform followPlayer;
    Animator npcAnim;
    public float walkSpeed = 3.0f;
    public CharacterController npcController;
    public Vector3 ctrlVelocity;
    float npcGrav = 0f;
    float npcPosition;

    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    public void Start()
    {
        npcController = gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        npcMoving();
        FloatUpDown();
    }

    public void npcMoving()
    {
        Vector3 direction = followPlayer.position - npcController.transform.position;
        float angle = Vector3.Angle(direction, npcController.transform.position);
        npcPosition = Vector3.Distance(followPlayer.position, npcController.transform.position);
        
        direction.y = 0;
        npcController.transform.rotation = Quaternion.Slerp(npcController.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        if (direction.magnitude > 1)
        {
            Debug.Log("NPC Position :- " + npcPosition);

            if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) && npcPosition > 10)
            {
                npcMove(direction, walkSpeed);
            }
        }
    }

    private void FloatUpDown()
    {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.Translate(Vector3.up * yOffset * Time.deltaTime);
    }

    void npcMove(Vector3 dir, float spd)
    {
        ctrlVelocity = dir.normalized * spd;
        ctrlVelocity.y = Mathf.Clamp(npcController.velocity.y, -30, 2);
        ctrlVelocity.y -= npcGrav * Time.deltaTime;
        npcController.Move(ctrlVelocity * Time.deltaTime);
    }
}