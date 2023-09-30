using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FrithControllerTest5 : MonoBehaviour
{
    public float speed;
    public float distance;

    public float verticalSpeed = 2f; // 위아래로 움직이는 속도
    public float verticalRange = 1f; // 위아래 움직임의 범위

    public LayerMask groundLayer;
    public Transform player;

    private Vector3 initialPosition;

    public float distanceBehindPlayer = 2.0f; // 플레이어 뒤의 거리를 조절
    private const float HeightAbovePlayer = 3.0f; // 플레이어보다 얼마나 위에 있을지 결정

   void Start()
   {
        player = GameObject.Find("Player").transform;
        initialPosition = transform.position + new Vector3(0, HeightAbovePlayer, 0); // 초기 위치 저장
   }

   void Update()
   {
       FloatUpDown(); 

       Vector3 targetPosition = player.position - player.forward * distanceBehindPlayer;
       targetPosition.y += HeightAbovePlayer;
       transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        
       Vector3 lookAtPosition = player.position;
       lookAtPosition.y += 1.5f; 
       transform.LookAt(lookAtPosition);

      if (Vector3.Distance(transform.position, player.position) < distance)
      {
           transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
           initialPosition.y=transform.position.y;//플레이어가 앞지르면 다시 초기위치 재설정.
      }
   }

   private void FloatUpDown()
   {
        float yOffset = Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
        transform.position=new Vector3(transform.position.x,initialPosition.y+yOffset ,transform.position.z);
   }
}
