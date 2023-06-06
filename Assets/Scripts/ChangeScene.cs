using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //public Animator crossFade;
    //public float transitionTime = 1f;
    [SerializeField] RectTransform fader;
    [SerializeField] RectTransform fader2;


    private void Start ()
    {
        fader.gameObject.SetActive(true);

        /*        LeanTween.scale(fader, new Vector3(1,1,1), 0);
                LeanTween.scale(fader, Vector3.zero, 0.5f).setOnComplete(() =>{
                fader.gameObject.SetActive(false);
        }*/
        LeanTween.alpha(fader, 1, 0);
        LeanTween.alpha(fader, 0, 0.5f).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }

    public void MoveToMenu()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete (() =>
         {
             // fader.gameObject.SetActive(false);
             //Invoke("sceneID", 0.5f);
             SceneManager.LoadScene(2);
         });
    }

    public void MoveToGame()
    {
        /*        fader.gameObject.SetActive(true);

                LeanTween.alpha(fader, 0, 0);
                LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
                {
                    // fader.gameObject.SetActive(false);
                    //Invoke("sceneID", 0.5f);
                    SceneManager.LoadScene(1);
                });*/
        fader2.gameObject.SetActive(true);

        LeanTween.scale(fader2, Vector3.zero, 0f);
        LeanTween.scale(fader2, new Vector3 (1, 1,1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadGame", 0.5f);
            //SceneManager.LoadScene(1);
        });

    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToFirst()
    {
        /*        fader.gameObject.SetActive(true);

                LeanTween.alpha(fader, 0, 0);
                LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
                {
                    // fader.gameObject.SetActive(false);
                    //Invoke("sceneID", 0.5f);
                    SceneManager.LoadScene(1);
                });*/
        fader2.gameObject.SetActive(true);

        LeanTween.scale(fader2, Vector3.zero, 0f);
        LeanTween.scale(fader2, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadFirst", 0.5f);
            //SceneManager.LoadScene(1);
        });

    }

/*    public void MoveToSettings()
    {
        fader2.gameObject.SetActive(true);

        LeanTween.scale(fader2, Vector3.zero, 0f);
        LeanTween.scale(fader2, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(3);
        });

    }
*/

    private void LoadFirst()
    {
        SceneManager.LoadScene(0);
    }

    /*    public void MoveToScene(int sceneID)
        {
            fader.gameObject.SetActive(true);

            LeanTween.alpha(fader, 0, 0);
            LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
            {
                // fader.gameObject.SetActive(false);
                //Invoke("sceneID", 0.5f);
                SceneManager.LoadScene(sceneID);
            });
        }*/
    /*    public void MoveToScene()
        {
            StartCoroutine(LoadMap(SceneManager.GetActiveScene().buildIndex+1));
        }*/

    /*    IEnumerator LoadMap(int levelIndex)
        {
            crossFade.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.MoveToScene(levelIndex);
        }*/
    /*    public void GameScenesCtrl()
        {
            SceneManager.
        }*/

}
