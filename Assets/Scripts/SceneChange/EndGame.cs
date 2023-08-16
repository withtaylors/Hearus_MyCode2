using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private ChangeScene changeScene;

    private void Start()
    {
        changeScene = FindObjectOfType<ChangeScene>();
    }
    
    public void ExitGame()
    {
        Time.timeScale = 1;
        //changeScene.MoveToFirst();
        ChangeScene.target2() ;
    }

    public void EndingGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}