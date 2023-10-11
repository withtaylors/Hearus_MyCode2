using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;
    public playerSound playerSound_;

   private void Awake()
   {
       playerSound_ = GameObject.FindObjectOfType<playerSound>();
   }

    public void Setting()
    {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
        playerSound_.PauseAllSounds();
    }

    public void Continue()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
        playerSound_.ResumeAllSounds();
    }
}