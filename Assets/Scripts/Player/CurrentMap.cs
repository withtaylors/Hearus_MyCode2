using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class CurrentMap : MonoBehaviour
{
    public TextMeshProUGUI stageText_1;
    public TextMeshProUGUI stageText_2;
    public TextMeshProUGUI stageText_3;

    public TextMeshProUGUI mapText_1;
    public TextMeshProUGUI mapText_2;
    public TextMeshProUGUI mapText_3;

    void OnCollisionEnter(Collision collision) // 현재 스테이지를 파악하기 위함
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

    private void UpdateCurrentStage(string _stage) // 현재 스테이지 변경
    {
        stageText_1.text = _stage;
        stageText_2.text = _stage;
        stageText_3.text = _stage;
    }

    public void UpdateCurrentMap(string _map) // 현재 맵 변경 -> 추후 씬이 변경될 떄 호출
    {
        mapText_1.text = _map;
        mapText_2.text = _map;
        mapText_3.text = _map;
    }
}
