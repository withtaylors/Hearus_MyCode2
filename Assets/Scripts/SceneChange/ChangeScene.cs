using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance; // 싱글톤 패턴을 위한 정적 인스턴스 필드

    [SerializeField] RectTransform fader;
    [SerializeField] RectTransform fader2;

    public static Action target;

    private void Start()
    {
        fader.gameObject.SetActive(true);
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
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(2);
        });
    }

    public void MoveToGame()
    {
        Debug.Log("여기까진옴");
        fader2.gameObject.SetActive(true);

        LeanTween.scale(fader2, Vector3.zero, 0f);
        LeanTween.scale(fader2, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadGame", 0.5f);
        });
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToFirst()
    {
        fader2.gameObject.SetActive(true);

        LeanTween.scale(fader2, Vector3.zero, 0f);
        LeanTween.scale(fader2, new Vector3(1, 1, 1), 2f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Invoke("LoadFirst", 0.5f);
        });
    }

    private void LoadFirst()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        target = () => { MoveToGame(); }; 
    }
}
