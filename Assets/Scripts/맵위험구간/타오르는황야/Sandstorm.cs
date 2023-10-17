using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : MonoBehaviour
{
    public GameObject boundingObject;
    public float speed = 2f;

    public BoxCollider boundingBox;
    public Vector3 targetPosition;

    public float damageInterval = 4f;  // 플레이어에게 데미지를 입히는 간격

    private float lastDamageTime;  // 마지막 데미지를 입힌 시간

    private void Start()
    {
        boundingBox = boundingObject.GetComponent<BoxCollider>();
        PickNewTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        Bounds bounds = boundingBox.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 트리거 내에 있고 일정 간격마다 데미지를 입히도록 체크
        if (other.CompareTag("Player") && Time.time - lastDamageTime >= damageInterval)
        {
            Debug.Log("Player 만남");
            // 여기에 플레이어의 HP를 감소시키는 코드 추가
            PlayerHP.instance.DecreaseHP(10);  //5의 데미지를 입힘
            lastDamageTime = Time.time;  // 데미지를 입힌 시간 업데이트
            Debug.Log("lastDamageTime >>> " + lastDamageTime);
        }
    }
}
