using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Yarn.Unity;
using System.Linq;

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
    public bool grounded=false;
    public bool isInDialogue = false;
    //picking 애니메이션을 실행 중인지 여부를 저장하는 변수
    public bool isPicking = false;
    public bool isJumping;

    public Transform character; // 등반자 캐릭터 Transform
    public Transform rope; // 로프 GameObject
    public bool isClimbing = false;
    private bool canClimb = false; // 로프와 상호 작용할 수 있는지 여부를 저장합니다.

    private Rigidbody playerRigidbody;

    // Ground 레이어 감지를 위함
    public LayerMask groundLayer;

    //private Collider[] colliders; // colliders 변수를 클래스 수준으로 이동하여 필드로 선언
    private List<GameObject> pickedItems = new List<GameObject>(); // 선택한 아이템을 저장하기 위한 리스트

    private ItemPickup itemPickup;

    private float ropeInteractionDistance = 1.5f; // 로프와 상호 작용하는 최대 거리
    public float climbSpeed = 1.0f;

    private string colliderTag;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        itemPickup = FindObjectOfType<ItemPickup>();
        //soundPlayer = GetComponent<playerSound>(); // playerSound 스크립트를 가져옴
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 로프 타기 토글
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleClimbing();
        }

        // 로프와의 거리 확인 및 클라이밍 상태 종료
        CheckRopeDistance();
        // 로프 클라이밍 처리
        if (isClimbing){
                    ProcessClimbing();

        }
    }

    void FixedUpdate()
    {
        // 로프 클라이밍 상태에서는 움직임 처리를 건너뛰기
        if (!isClimbing)
        {
            HandleMovement(); //플레이어 Movement
        }

        HandleJump(); //플레이어 점프
        CheckPicking(); //아이템 줍기
    }

void HandleMovement()
    {
     // 애니메이션/대화 실행 중이거나 isPicking 중일 때, 애니메이션 실행중일 시 움직임을 막음
    if (isPicking || isInDialogue || isClimbing || myAnim.GetCurrentAnimatorStateInfo(0).IsName("falling to land") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Taking") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Picking"))
    {
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isRunning", false);

        myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
        return;
    }
    
        //방향키 감지
        float hmove = Input.GetAxis("Horizontal");
        float vmove = Input.GetAxis("Vertical");

        //어떤 방향키든지 감지해서 걷는 animation 실행
        isWalking = hmove != 0 || vmove != 0;
        myAnim.SetBool("isWalking", isWalking);

		// 수정된 부분: 방향키를 누른 상태에서만 isRunning이 true가 됩니다.
        isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        myAnim.SetBool("isRunning", isRunning);

        //걷기, 뛰기 속도를 사용자 설정에 따르게 함
        float speed = isRunning ? runSpeed : walkSpeed;

        //플레이어 이동
        Vector3 moveDirection = new Vector3(-hmove, 0, -vmove).normalized;
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

    void HandleJump()
    {
        // 애니메이션/대화 실행 중일 때는 점프를 막음
        if (isPicking || isInDialogue)
            return;

        // 스페이스바를 눌렀을 시 점프 실행
        if (grounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            // grounded - false (땅과 닿지않음) 이므로 jump animation 실행
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    public void SetCanClimb(bool value)
    {
        canClimb = value;
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
    
    // void OnCollisionExit(Collision collision)
    // {
    //     // GroundLayer와 떨어졌다면
    //     if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
    //     {
    //         if (isClimbing == false){
    //         // grounded - false (땅과 닿지않음)
    //         grounded = false;
    //         myAnim.SetBool("grounded", grounded);
    //         }
    //         else {
                
    //         }
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     colliderTag = other.tag;
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     colliderTag = "";
    // }
    

    void CheckPicking()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f); // 주변 2 유닛 반경의 충돌체 확인

        if (!isPicking)
        {
            foreach (Collider collider in colliders)
            {
                colliderTag = collider.tag;

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
                    if (colliderTag == "ITEM_VINE" || colliderTag == "ITEM_DOLL" || colliderTag == "ITEM_NE_MUSHROOM" || colliderTag == "ITEM_BUTTERNUT")
                    {
                        Debug.Log("충돌한 태그: " + colliderTag + " 감지"); // E 키를 누르지 않은 상태에서 충돌한 태그 출력
                    }
                }
            }
        }
    }

    private void ToggleClimbing()
{
    // 밧줄과의 거리를 계산합니다.
    float distanceToRope = Vector3.Distance(transform.position, rope.position);

    // 밧줄에서 설정된 거리 이내에 있고 키를 누르면 밧줄을 타거나 내립니다.
    if (Input.GetKeyDown(KeyCode.C) && distanceToRope <= ropeInteractionDistance)
    {
        Debug.Log("C 누름");
        isClimbing = !isClimbing;
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isRunning", false);
        myAnim.SetBool("isClimbing", isClimbing);

    if (!isClimbing)
    {
        myRB.useGravity = true;
        myAnim.SetBool("isWalking", false);
        myAnim.SetBool("isRunning", false);
    }
    else
    {
        myRB.useGravity = false;
        myRB.velocity = Vector3.zero;
        myAnim.SetBool("isClimbing", isClimbing);
    }
    }
}


    private void CheckRopeDistance()
    {
        if (isClimbing && !canClimb && !grounded)
        {
            isClimbing = false;
            myAnim.SetBool("isClimbing", isClimbing);
            myRB.useGravity = true;
        }
    }

private void ProcessClimbing()
{
    if (isClimbing)
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(verticalInput) > 0)
        {
            myAnim.SetFloat("ClimbSpeed", Mathf.Abs(climbSpeed * verticalInput));
            Vector3 climbMovement = new Vector3(0, verticalInput * climbSpeed * Time.deltaTime, 0);
            transform.position += climbMovement;
        }
        else
        {
            myAnim.SetFloat("ClimbSpeed", 0);  // 방향키가 눌리지 않은 경우 애니메이션 속도를 0으로 설정
        }
    }
    else
    {
        myAnim.SetFloat("ClimbSpeed", 0);
    }
}



    IEnumerator ResetPicking(GameObject item)
    {
        // 애니메이션 재생 후 대기할 시간 설정
        float animationDuration = 2f; // 애니메이션 재생 시간 (초)
        yield return new WaitForSeconds(animationDuration);

        // 일정 시간이 지난 후에 isPicking을 다시 false로 설정
        isPicking = false;

        // 아이템 습득 및 오브젝트 제거
        //itemPickup.Pickup(item);
        item.GetComponent<ItemPickup>().Pickup();
        Destroy(item);
    }
}