using UnityEngine;
using System.Collections;

public class TimeOut : MonoBehaviour
{
    // 씬에 머문 시간을 추적하는 변수
    private float timeSpent = 0f;
    private bool hasDecreasedHP = false;

    void Update()
    {
        // 시간 측정
        timeSpent += Time.deltaTime;

        // 3분(180초)이 지났는지 확인하고 hp 감소
        if (timeSpent >= 180f && !hasDecreasedHP)
        {
            PlayerHP.instance.DecreaseHP(10);    
            hasDecreasedHP = true; // HP를 감소시켰다는 표시
            Debug.Log("hp-10");
        }
    }
}