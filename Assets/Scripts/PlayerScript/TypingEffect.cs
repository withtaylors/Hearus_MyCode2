using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TypingEffect : MonoBehaviour
{
    public static TypingEffect instance;
    public TextMeshProUGUI text;

    private void Awake()
    {
        instance = this;
    }

    public static void TMPDOText(TextMeshProUGUI _text, float duration)
    {
        _text.maxVisibleCharacters = 0;
        DOTween.To(x => _text.maxVisibleCharacters = (int)x, 0f, _text.text.Length, duration);
    }
}
