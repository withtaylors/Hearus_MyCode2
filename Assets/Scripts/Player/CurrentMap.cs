using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;

public class CurrentMap : MonoBehaviour
{
    public TextMeshProUGUI stageText_1;
    public TextMeshProUGUI stageText_2;
    public TextMeshProUGUI stageText_3;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("중심들판_Border_1")) // 중심들판 <-> 빽빽한숲
            UpdateCurrentStage("중심 들판");

        if (collision.gameObject.name.Equals("빽빽한숲_Border_1")) // 중심들판 <-> 빽빽한숲
            UpdateCurrentStage("빽빽한 숲");

        if (collision.gameObject.name.Equals("빽빽한숲_Border_2")) // 빽빽한숲 <-> 시냇물숲
            UpdateCurrentStage("빽빽한 숲");

        if (collision.gameObject.name.Equals("시냇물숲_Border_1")) // 빽빽한숲 <-> 시냇물숲
            UpdateCurrentStage("시냇물이 흐르는 숲");
    }

    private void UpdateCurrentStage(string _stage)
    {
        stageText_1.text = _stage;
        stageText_2.text = _stage;
        stageText_3.text = _stage;
    }
}
