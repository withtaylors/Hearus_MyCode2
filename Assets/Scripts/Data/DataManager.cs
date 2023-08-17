using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Item;
using System;

public class PlayerData
{
    public string filename;

    //플레이어 위치
    public float x = 40.6f;
    public float y = 8.5f;
    public float z = 0.5f;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤패턴

    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성
    public PlayerData nowPlayerDefault = new PlayerData(); // 기본 플레이어 데이터 생성

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        // 기본 플레이어 위치 설정
        nowPlayerDefault.x = 40.6f;
        nowPlayerDefault.y = 8.5f;
        nowPlayerDefault.z = 0.5f;

        path = Application.persistentDataPath + "/save";	// 경로 지정
        print(path);
    }

    public void SaveData()
    {
        if (File.Exists(path + nowSlot.ToString() + "_player.json")) // 파일이 이미 있는 경우
        {
            // 기존 파일에서 'filename' 속성만 가져옴
            string existingFileData = File.ReadAllText(path + nowSlot.ToString() + "_player.json");
            PlayerData existingPlayerData = JsonUtility.FromJson<PlayerData>(existingFileData);

            // 현재 파일에 'filename'을 적용하고 저장
            nowPlayer.filename = existingPlayerData.filename;
        }

        string playerData = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString() + "_player.json", playerData);
    }


    public void LoadData()
    {
        string playerData = File.ReadAllText(path + nowSlot.ToString() + "_player.json");
        nowPlayer = JsonUtility.FromJson<PlayerData>(playerData);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}