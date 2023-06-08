using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasgroup;
    public bool fade_in = false;
    public bool fade_out = false;
    //int sceneID;

    public float TimeToFade;
    public float TimeToChangeScene;

    // Update is called once per frame
    void Update()
    {
        if (fade_in)
        {
            if(canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += Time.deltaTime;
                if(canvasgroup.alpha >= 1)
                {
                    fade_in = false;
                }
            }
        }

        if (fade_out)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= Time.deltaTime;
                if (canvasgroup.alpha == 0)
                {
                    fade_in = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fade_in = true;
    }

    public void FadeOut()
    {
        fade_out = true;
    }

    public void ChangeScene()
    {
        StartCoroutine(FadeOutScreenChange());
    }

    IEnumerator FadeOutScreenChange()
    {
        FadeOut();
        yield return new WaitForSeconds(TimeToChangeScene);
        SceneManager.LoadScene("MainGame");
    }
}
