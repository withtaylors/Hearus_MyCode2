using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : MonoBehaviour {
    public GameObject boundingObject;
    public float speed = 2f;

    public BoxCollider boundingBox;
    public Vector3 targetPosition;

    private void Start() {
        boundingBox = boundingObject.GetComponent<BoxCollider>();
        PickNewTarget();
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f) {
            PickNewTarget();
        }
    }

    private void PickNewTarget() {
        Bounds bounds = boundingBox.bounds;
        
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
   }
}
