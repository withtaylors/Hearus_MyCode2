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

        if (collision.gameObject.name.Equals("1_강과바다_바닥_큐브") || collision.gameObject.name.Equals("Island_A_Cube") || collision.gameObject.name.Equals("4_물도시_바닥_Cube"))
        {
            DataManager.instance.nowPlayer.gameNext = false;    
            Debug.Log("collision - gameNext = false");  
        }

        if (collision.gameObject.name.Equals("Island_D_Cube.041") || collision.gameObject.name.Equals("3_물도시_바닥_Bridge"))
        {
            DataManager.instance.nowPlayer.gameBefore = false;    
            Debug.Log("collision - gameBefore = false");             
        }
        
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("NextStage"))
        {
            if(DataManager.instance.nowPlayer.currentMap == "태초의숲")
            {
                DataManager.instance.nowPlayer.currentMap = "비탄의바다";   
            }
            else if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
            {
                DataManager.instance.nowPlayer.currentMap = "타오르는황야";
            }

            DataManager.instance.nowPlayer.gameNext = true;
            Debug.Log("CurrentMap에서 gameNext true로 변경");

            DataManager.instance.SaveData(DataManager.instance.nowSlot);
            ChangeScene.target4();
        }

        else if (other.gameObject.name.Equals("BeforeStage"))
        {
            if(DataManager.instance.nowPlayer.currentMap == "비탄의바다")
            {
                DataManager.instance.nowPlayer.currentMap = "태초의숲"; 
            }   
            else if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
            {
                DataManager.instance.nowPlayer.currentMap = "비탄의바다";
            }

            DataManager.instance.nowPlayer.gameBefore = true;
            Debug.Log("CurrentMap에서 gameBefore true로 변경");

            DataManager.instance.SaveData(DataManager.instance.nowSlot);
            ChangeScene.target4();
        }
    }
}