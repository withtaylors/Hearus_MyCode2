using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrithControllerTest3 : MonoBehaviour
{
    public float attackSpeed = 4;
    public float attackDistance;
    public float bufferDistance;
    public GameObject player;

    Transform playerTransform;

    void GetPlayerTransform()
    {
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.Log("Player not specified in Inspector");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        // Debug.Log("Distance to Player" + distance);
        if (distance <= attackDistance)
        {
            if (distance >= bufferDistance)
            {
                transform.position += transform.forward * attackSpeed * Time.deltaTime;
            }
        }
    }
}