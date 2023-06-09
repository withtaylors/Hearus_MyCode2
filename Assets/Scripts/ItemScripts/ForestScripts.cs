using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForestScripts : MonoBehaviour
{
    // 태초의 숲 내 아이템을 탐사하면 나오는 텍스트들을 담은 스크립트.

    public static ForestScripts instance;

    public TextMeshProUGUI Script;

    private void Awake()
    {
        instance = this;
    }
    


}
