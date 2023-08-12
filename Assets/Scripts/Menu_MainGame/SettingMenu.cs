using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;
    //public Button quitButton;

    private void Start()
    {
        /*var buttonComponent = quitButton.GetComponent<UnityEngine.UI.Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(End);
        }
        else
        {
            Debug.LogError("quitButton does not have a Button component.");
        }*/
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

    /*public void End()
    {
        ChangeScene.target2();
    }*/
}