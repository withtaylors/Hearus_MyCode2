using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Yarn.Unity;

public class playerController : MonoBehaviour
{
    //걷는속도, 뛰는속도, 점프높이
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;

    public Rigidbody myRB;
    public Animator myAnim;

    //걷기, 뛰기, 점프를 위한 땅 착지 여부를 확인할 수 있는 grounded 변수
    public bool isWalking = false;
    public bool isRunning = false;
    public bool grounded;
    public bool isInDialogue = false;
    //picking 애니메이션을 실행 중인지 여부를 저장하는 변수
    public bool isPicking = false;

    // Ground 레이어 감지를 위함
    public LayerMask groundLayer;

    //private Collider[] colliders; // colliders 변수를 클래스 수준으로 이동하여 필드로 선언
    private List<GameObject> pickedItems = new List<GameObject>(); // 선택한 아이템을 저장하기 위한 리스트

    private ItemPickup itemPickup;

    //private playerSound soundPlayer; // playerSound 스크립트를 참조하기 위한 변수

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        itemPickup = FindObjectOfType<ItemPickup>();
        //soundPlayer = GetComponent<playerSound>(); // playerSound 스크립트를 가져옴
    }

    void FixedUpdate()
    {
        //IsInDialogue();

        //if (isInDialogue)
        //    return;

        //soundPlayer.HandleMovement(isWalking, isRunning); // playerSound 스크립트의 HandleMovement 메서드 호출

        HandleMovement(); //플레이어 Movement
        HandleJump(); //플레이어 점프
        CheckPicking(); //아이템 줍기
    }

    void HandleMovement()
    {
        // 애니메이션/대화 실행 중일 때는 움직임을 막음
        if (isPicking || isInDialogue)
        {
            isWalking = false;
            myAnim.SetBool("isWalking", false);
            return;
        }

        //방향키 감지
        float hmove = Input.GetAxis("Horizontal");
        float vmove = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        //어떤 방향키든지 감지해서 걷는 animation 실행
        isWalking = hmove != 0 || vmove != 0;
        myAnim.SetBool("isWalking", isWalking);
        //shift감지 했다면 뛰는 animation 실행
        myAnim.SetBool("isRunning", isRunning);

        //걷기, 뛰기 속도를 사용자 설정에 따르게 함
        float speed = isRunning ? runSpeed : walkSpeed;

        //플레이어 이동
        Vector3 moveDirection = new Vector3(-vmove, 0, hmove).normalized;
        //플레이어 이동 속도
        Vector3 velocity = moveDirection * speed;
        velocity.y = myRB.velocity.y;
        myRB.velocity = velocity;

        //플레이어 이동방향에 따라 캐릭터 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            myRB.rotation = Quaternion.Slerp(myRB.rotation, newRotation, 10f * Time.deltaTime);
        }
    }

    //플레이어 점프
    void HandleJump()
    {
        // 애니메이션/대화 실행 중일 때는 점프를 막음
        if (isPicking || isInDialogue)
            return;

        //스페이스바를 눌렀을 시 점프 실행
        if (grounded && Input.GetButtonDown("Jump"))
        {
            //grounded - false (땅과 닿지않음) 이므로 jump animation 실행
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    //플레이어 점프 - 땅과 충돌 감지
    void OnCollisionEnter(Collision collision)
    {
        //GroundLayer와 만났다면 
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            //grounded - true (땅과 닿았음) 이므로 falling to land animation 실행
            grounded = true;
            myAnim.SetBool("grounded", grounded);
        }
    }

    void CheckPicking()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f); // 주변 2 유닛 반경의 충돌체 확인

        if (!isPicking)
        {
            foreach (Collider collider in colliders)
            {
                // 충돌한 객체의 태그 저장
                string colliderTag = collider.tag;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (colliderTag == "ITEM_VINE")
                    {
                        // "isTaking" 애니메이션 실행
                        myAnim.SetTrigger("isTaking");
                        isPicking = true; // picking 애니메이션이 실행 중임을 표시
                        StartCoroutine(ResetPicking(collider.gameObject)); // 일정 시간 후에 isPicking을 다시 false로 설정하고 아이템 삭제하는 코루틴 시작
                        break; // 첫 번째로 발견된 아이템만 처리하고 반복문 종료
                    }

                    if (colliderTag == "ITEM_DOLL" || colliderTag == "ITEM_NE_MUSHROOM")
                    {
                        // "isPicking" 애니메이션 실행
                        myAnim.SetTrigger("isPicking");
                        isPicking = true; // picking 애니메이션이 실행 중임을 표시
                        StartCoroutine(ResetPicking(collider.gameObject)); // 일정 시간 후에 isPicking을 다시 false로 설정하고 아이템 삭제하는 코루틴 시작
                        break; // 첫 번째로 발견된 아이템만 처리하고 반복문 종료
                    }
                }
                else
                {
                    if (colliderTag == "ITEM_VINE" || colliderTag == "ITEM_DOLL" || colliderTag == "ITEM_NE_MUSHROOM")
                    {
                        // Debug.Log("충돌한 태그: " + colliderTag + " 감지"); // E 키를 누르지 않은 상태에서 충돌한 태그 출력
                    }
                }
            }
        }
    }

    IEnumerator ResetPicking(GameObject item)
    {
        // 애니메이션 재생 후 대기할 시간 설정
        float animationDuration = 2f; // 애니메이션 재생 시간 (초)
        yield return new WaitForSeconds(animationDuration);

        // 일정 시간이 지난 후에 isPicking을 다시 false로 설정
        isPicking = false;

        // 아이템 삭제
        //        Destroy(item);
        //        pickedItems.Add(item); // 선택한 아이템 리스트에 추가

        itemPickup.Pickup(item);

        Destroy(item);
    }
}
