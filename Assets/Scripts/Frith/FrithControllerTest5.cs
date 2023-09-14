using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FrithControllerTest5 : MonoBehaviour {

    private NavMeshAgent enemy;
    public Transform PlayerTarget;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
     enemy.SetDestination(PlayerTarget.position);
    }
}