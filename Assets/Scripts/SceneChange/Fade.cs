using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] RectTransform fader3;
    void Start()
    {
        fader3.gameObject.SetActive(true);
        LeanTween.alpha(fader3, 1, 0);
        LeanTween.alpha(fader3, 0, 3f).setOnComplete(() =>
        {
            fader3.gameObject.SetActive(false);
        }); 
    }

    public void FadeIn()
    {
        fader3.gameObject.SetActive(true);
        LeanTween.alpha(fader3, 0, 0); // 완전 투명한 상태로 시작
        LeanTween.alpha(fader3, 1, 3f).setOnComplete(() => // 점점 불투명해지며(페이드 인)
        {
            fader3.gameObject.SetActive(false);
        });         
    }
}
