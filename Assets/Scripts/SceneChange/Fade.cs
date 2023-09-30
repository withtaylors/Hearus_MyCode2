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
}
