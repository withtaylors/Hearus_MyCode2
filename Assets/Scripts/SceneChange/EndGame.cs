using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}