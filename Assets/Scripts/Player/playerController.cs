using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System.Linq;
using UnityEngine.Events;
using TMPro;

public class playerController : MonoBehaviour
{
    public Rigidbody myRB;
    public Animator myAnim;
    private Rigidbody playerRigidbody;
    public LayerMask groundLayer; // Ground 레이어 감지를 위함

    //걷는속도, 뛰는속도, 점프높이
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;

    //걷기, 뛰기, 점프를 위한 땅 착지 여부를 확인할 수 있는 grounded 변수
    public bool isWalking = false;
    public bool isRunning = false;
    public bool grounded=false;
    //picking 애니메이션을 실행 중인지 여부를 저장하는 변수
    public bool isPicking = false;

    public Transform character; // 등반자 캐릭터 Transform
    public Transform rope; // 로프 GameObject
    public bool isClimbing = false;
    public bool canUseRope = false;
    private bool canClimb = false; // 로프와 상호 작용할 수 있는지 여부를 저장

    // 밧줄관련 변수
    private float ropeInteractionDistance = 1.5f; // 로프와 상호 작용하는 최대 거리
    public float climbSpeed = 1.0f; // 밧줄 타는 속도

    private List<GameObject> pickedItems = new List<GameObject>(); // 선택한 아이템을 저장하기 위한 리스트
    private ItemPickup itemPickup;
    private string colliderTag;

    // Script 관련 변수
    public bool isPlayingScript = false;

    public UnityEvent useRope;
    public UnityEvent arriveRopeField;

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        itemPickup = FindObjectOfType<ItemPickup>();
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
        isPlayingScript = ScriptManager.instance.isPlayingScript;

        // 애니메이션/대화 실행 중이거나 isPicking 중일 때, 애니메이션 실행중일 시 움직임을 막음
        if (isPicking || isPlayingScript || isClimbing || myAnim.GetCurrentAnimatorStateInfo(0).IsName("falling to land") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Taking") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("Picking"))
        {
            myAnim.SetBool("isWalking", false);
            myAnim.SetBool("isRunning", false);

            myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
            return;
        }
    
        //방향키 감지
        float hmove = Input.GetAxis("Horizontal");
        float vmove = Input.GetAxis("Vertical");

        //grounded이며 방향키를 누른 상태에서만 isWalking이 true가 됨
        isWalking = grounded && (hmove != 0 || vmove != 0);
        myAnim.SetBool("isWalking", isWalking);

        //grounded이며 방향키를 누른 상태에서만 isRunning이 true가 됨
        isRunning = grounded && isWalking && Input.GetKey(KeyCode.LeftShift);
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
        if (isPicking || isPlayingScript)
            return;

        if (!grounded) // 플레이어가 땅과 닿지 않은 상태일 때
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
        }
        else // 플레이어가 땅에 닿은 상태일 때
        {
            // 스페이스바를 눌렀을 시 점프 실행
            if (grounded && Input.GetButtonDown("Jump"))
            {
                // grounded - false (땅과 닿지않음) 이므로 jump animation 실행
                grounded = false;
                myAnim.SetBool("grounded", grounded);

                myRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }

            // if (grounded == true && myAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            // {
            //     myAnim.SetBool("grounded", grounded);
            //     myAnim.Play("falling to land");
            // }
        }

        // if (grounded == true && myAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        // {
        //     myAnim.SetBool("grounded", grounded);
        // }
    }

    //플레이어 점프 - 땅과 충돌 감지
    void OnCollisionEnter(Collision collision)
    {
        //GroundLayer와 만났다면 
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
        {
            // 플레이어가 착지하였으므로 grounded - true (땅과 닿았음)
            grounded = true;
            myAnim.SetBool("grounded", grounded);
        }

        if (collision.gameObject.name.Equals("중심들판_Border_1")) // 로프를 사용하여 목적지에 닿으면 Invoke() -> 튜토리얼 컨트롤러에 전달됨
            useRope.Invoke();

        if (collision.gameObject.name.Equals("Island_A_Cube.007")) // 로프를 사용해야 하는 곳에 도달하면 Invoke() -> 튜토리얼 컨트롤러에 전달됨
        {
            canUseRope = true;
            arriveRopeField.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Equals("Island_A_Cube.007")) // 로프를 사용해야 하는 곳에서 벗어났을 때
            canUseRope = false;
    }

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

                    if (colliderTag == "ITEM_DOLL" || colliderTag == "ITEM_NE_MUSHROOM" || colliderTag == "ITEM_BUTTERNUT" || colliderTag == "ITEM_PICKING")
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
                    if (colliderTag == "ITEM_VINE" || colliderTag == "ITEM_DOLL" || colliderTag == "ITEM_NE_MUSHROOM" || colliderTag == "ITEM_BUTTERNUT" || colliderTag == "ITEM_PICKING")
                    {
                        Debug.Log("충돌한 태그: " + colliderTag + " 감지"); // E 키를 누르지 않은 상태에서 충돌한 태그 출력
                    }
                }
            }
        }
    }

    private void ToggleClimbing()
    {
        if (isClimbing)
        {
            isClimbing = false;
            myAnim.SetBool("isClimbing", isClimbing);
            myRB.useGravity = true;
            myAnim.speed = 1; //애니메이션 속도를 복원
        }
        else
        {
            float distanceToRope = Vector3.Distance(transform.position, rope.position);

            if (distanceToRope <= ropeInteractionDistance)
            {
                isClimbing = true;
                myAnim.SetBool("isClimbing", isClimbing);
                myRB.useGravity = false;
                myRB.velocity = Vector3.zero;

                // isWalking과 isRunning을 false로 설정
                isWalking = false;
                isRunning = false;
                myAnim.SetBool("isWalking", isWalking);
                myAnim.SetBool("isRunning", isRunning);
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
                myAnim.speed = Mathf.Abs(climbSpeed * verticalInput);
                Vector3 climbMovement = new Vector3(0, verticalInput * climbSpeed * Time.deltaTime, 0);
                transform.position += climbMovement;
            }
            else
            {
                // 애니메이션이 0.5초 위치에서 멈추도록 설정
                myAnim.Play("Climbing", 0, 0.5f);
                myAnim.speed = 0;
                if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("Climbing"))
                {
                    myAnim.Play("Climbing", 0, 0.5f);
                }
            }
        }
        else
        {
            myAnim.speed = 1;
        }
    }

    IEnumerator ResetPicking(GameObject item)
    {
        // 애니메이션 재생 후 대기할 시간 설정
        float animationDuration = 2f; // 애니메이션 재생 시간 (초)
        yield return new WaitForSeconds(animationDuration);

        // 일정 시간이 지난 후에 isPicking을 다시 false로 설정
        isPicking = false;

        // 아이템 아이디 전달받기
        int go_itemID = item.GetComponent<ItemPickup>()._itemID;
        ScriptManager.instance.currentGameObject = item;

        // 해당 아이템 스크립트를 찾고 재생
        ScriptManager.instance.FindScriptByItemID(go_itemID);
        ScriptManager.instance.ShowScript();
    }

    public void UpdateGrounded(bool isCollidingWithGround)
    {
        grounded = false;
        myAnim.SetBool("grounded", grounded);
        Debug.Log("여기까지옴");
    }
}