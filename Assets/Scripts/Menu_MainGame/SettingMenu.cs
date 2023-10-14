using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;
    public playerController player;

    void Awake()
    {
        player = FindObjectOfType<playerController>();
    }

    public void Setting()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
        player.isWalking = false;
        player.isRunning = false;
    }

    public void Continue()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
        player.isWalking = true;
        player.isRunning = true;
    }
}