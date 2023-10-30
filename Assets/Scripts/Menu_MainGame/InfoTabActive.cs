using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTabActive : MonoBehaviour
{
    public GameObject InfoTab;
    public List<GameObject> otherTab = new List<GameObject>();
    public playerController player;

    private bool infoTabIsActive = false;
    void Awake()
    {
        player = FindObjectOfType<playerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InfoTab.SetActive(!infoTabIsActive);

            if (infoTabIsActive)
            {
                Time.timeScale = 0; // 게임 일시 중지
                player.isWalking = false;
                player.isRunning = false;
            }
            else
            {
                Time.timeScale = 1; // 게임 재개
                player.isWalking = true;
                player.isRunning = true;
            }

            // otherTab 리스트에 있는 모든 GameObject의 활성화 상태를 토글
            foreach (GameObject obj in otherTab)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
