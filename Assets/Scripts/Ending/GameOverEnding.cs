using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverEnding : MonoBehaviour
{
    public static GameOverEnding instance;

    public GameObject fader;
    public ScriptManager scriptManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (DataManager.instance.nowPlayer.playerHP == 0)
            GameOver();
    }

    public void GameOver()
    {
        StartCoroutine("FadeOutScene");

        if (DataManager.instance.nowPlayer.endingNumber == 0)
        {
            DataManager.instance.nowPlayer.gameOver = true;
            DataManager.instance.nowPlayer.endingNumber = Random.Range(1, 3);
        }

        switch (DataManager.instance.nowPlayer.endingNumber)
        {
            case 1:
                scriptManager.FindScriptByEventName("GAMEOVER_1"); // 스크립트 재생
                scriptManager.ShowScript();
                break;
            case 2:
                scriptManager.FindScriptByEventName("GAMEOVER_2"); // 스크립트 재생
                scriptManager.ShowScript();
                break;
            case 3:
                scriptManager.FindScriptByEventName("GAMEOVER_3"); // 스크립트 재생
                scriptManager.ShowScript();
                break;
        }
    }

    public void MoveToMain()
    {
        if (scriptManager.currentScript.eventName.Equals("GAMEOVER_1") ||
            scriptManager.currentScript.eventName.Equals("GAMEOVER_2") ||
            scriptManager.currentScript.eventName.Equals("GAMEOVER_3"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator FadeOutScene()
    {
        fader.SetActive(true);

        Color c = fader.GetComponent<Image>().color;

        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            c.a = f;

            fader.GetComponent<Image>().color = c;
        }

        yield return null;
    }
}
