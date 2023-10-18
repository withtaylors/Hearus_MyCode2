using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 HP를 구현한 스크립트
public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    public float HP = 100;
    public int intHP;

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
        // 현재 HP가 0 이하이면 더이상 HP를 감소시키지 않도록 조건 추가
        if (HP <= 0)
        {
            return;
        }

        switch (DataManager.instance.nowPlayer.nowCharacter)
            {
                case "Eden":
                    if (DataManager.instance.nowPlayer.currentMap == "태초의숲" || DataManager.instance.nowPlayer.currentMap == "종말과XXX")
                    {
                        HP -= (value / 2);
                        Debug.Log("value / 2" + HP);
                    }
                    else
                    {
                        HP -= value;
                        Debug.Log("value" + HP);
                    }
                    break;
                case "Noah":
                    if (DataManager.instance.nowPlayer.currentMap == "비탄의바다")
                    {
                        HP -= (value / 2);
                    }
                    else
                    {
                        HP -= value;
                    }
                    break;
                case "Adam":
                    if (DataManager.instance.nowPlayer.currentMap == "타오르는황야")
                    {
                        HP -= (value / 2);
                    }
                    else
                    {
                        HP -= value;
                    }
                    break;
                case "Jonah":
                    HP -= value;
                    break;
                case "None":
                    HP -= value;
                    break;
            }
        
        intHP = Mathf.FloorToInt(HP);
        SetActiveHPBar(intHP);
        DataManager.instance.nowPlayer.playerHP = intHP;
        //SetActiveHPBar(HP);
        //DataManager.instance.nowPlayer.playerHP = HP;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void IncreaseHP(int value)
    {
        // 현재 HP가 100 이상이 되지 않도록 조건 추가
        if (HP >= 100)
        {
            return;
        }

        HP += value;

        // 현재 HP가 100을 초과하지 않도록 보정
        if (HP > 100)
        {
            HP = 100;
        }

        intHP = Mathf.FloorToInt(HP);
        SetActiveHPBar(intHP);
        DataManager.instance.nowPlayer.playerHP = intHP;        
        // SetActiveHPBar(HP);
        // DataManager.instance.nowPlayer.playerHP = HP;
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

        HP_Text.text = Mathf.FloorToInt(_HP).ToString(); // 소수점을 버림
        HP_Text2.text = Mathf.FloorToInt(_HP).ToString();
    }
}
