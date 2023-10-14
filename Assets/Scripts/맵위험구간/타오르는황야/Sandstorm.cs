using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sandstorm : MonoBehaviour {
    public float speed = 3f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    private Vector3 targetPosition;

    private void Start() {
        PickNewTarget();
    }

    private void Update() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f) {
            PickNewTarget();
        }
    }

   private void PickNewTarget() {
       float randomX = Random.Range(minX, maxX);
       float randomZ = Random.Range(minZ, maxZ);

       // 현재 Y 축의 위치를 유지하기 위해 transform.position.y를 사용합니다.
       // 만약 다른 높이에서 움직이게 하려면 이 부분을 적절히 수정하세요.
       targetPosition = new Vector3(randomX, transform.position.y, randomZ);
   }
}
