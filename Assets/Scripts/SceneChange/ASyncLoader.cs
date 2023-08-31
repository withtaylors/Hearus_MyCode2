using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    // [Header("Menu Screens")]
    // [SerializeField] private GameObject loadingScreen;
    // [SerializeField] private GameObject ScreenCanvas;

    // [Header("Slider")]
    // [SerializeField] private Slider loadingSlider;

    // public void LoadLevelBtn()
    // {
    //     ScreenCanvas.SetActive(false);
    //     loadingScreen.SetActive(true);

    //     StartCoroutine(LoadAsync());
    // }

    // IEnumerator LoadAsync( )
    // {
    //     AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);

    //     while (!loadOperation.isDone)
    //     {
    //         float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
    //         loadingSlider.value = progressValue;

    //         yield return null;
    //     }
    // }
}
