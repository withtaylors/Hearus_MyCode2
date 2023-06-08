using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;

    void Update()
    {

    }

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
