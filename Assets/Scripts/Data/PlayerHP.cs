using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 플레이어 HP를 구현한 스크립트.

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;

    public int HP = 100;

    private List<Image> HPBars;
    public Transform tf_HPBars;

    public TextMeshProUGUI HP_Text;
    public TextMeshProUGUI HP_Text2;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        if (DataManager.instance.nowPlayer.playerHP != HP)
        {
            Debug.Log(" if (DataManager.instance.nowPlayer.playerHP != HP)");
            HP = DataManager.instance.nowPlayer.playerHP;
            DataManager.instance.SaveData(DataManager.instance.nowSlot);
        }

        HPBars = new List<Image>(tf_HPBars.GetComponentsInChildren<Image>());

        HP_Text.text = HP.ToString();
        HP_Text2.text = HP.ToString();
    }

    public void DecreaseHP(int value)
    {
        HP -= value;

        SetActiveHPBar(HP);
        DataManager.instance.nowPlayer.playerHP = HP;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void IncreaseHP(int value)
    {
        HP += value;

        SetActiveHPBar(HP);
        DataManager.instance.nowPlayer.playerHP = HP;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void SetActiveHPBar(int _HP)
    {
        for (int i = 0; i < HPBars.Count; i++)
        {
            Color color = HPBars[i].color;

            if (i < _HP / 5)
                color.a = 1f;
            else
                color.a = 0f;

            HPBars[i].color = color;
        }

        HP_Text.text = HP.ToString();
        HP_Text2.text = HP.ToString();
    }
}
