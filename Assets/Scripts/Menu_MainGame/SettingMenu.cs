using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;

    public void Setting()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
    }
}