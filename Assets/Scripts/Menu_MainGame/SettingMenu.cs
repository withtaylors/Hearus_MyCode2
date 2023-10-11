using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;
    private playerController player;

    void Start()
    {
        player = FindObjectOfType<playerController>();
    }

    public void Setting()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
        player.isWalking = false;
        player.isRunning = false;
        player.isClimbing = false;
    }

    public void Continue()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
        player.isWalking = true;
        player.isRunning = true;
        player.isClimbing = true;
    }
}