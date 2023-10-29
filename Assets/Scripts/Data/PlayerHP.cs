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
    public int intHP = 100;

    private List<Image> HPBars;
    public Transform tf_HPBars;

    public TextMeshProUGUI HP_Text;
    public TextMeshProUGUI HP_Text2;

    private float colorChangeDelay = 3.0f;  //색상 변경 지연 시간
    private Sprite originalSprite;  // 원래 이미지 저장 변수
    public Sprite increaseSprite; //이미지 변경을 위한 스프라이트 변수
    public Sprite decreaseSprite;

    public static bool HPIncreased = false;   
    public static bool HPDecreased = false;   

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

        for (int i = 0; i < HPBars.Count; i++)
        {
            Color color = HPBars[i].color;

            if (i < HP / 5)
                color.a = 1f;
            else
                color.a = 0f;

            HPBars[i].color = color;
        }

        HP_Text.text = HP.ToString();
        HP_Text2.text = HP.ToString();

        // 초기 이미지 저장
        originalSprite = HPBars[0].sprite;
    }

    public void DecreaseHP(int value)
    {
        // 현재 HP가 0 이하이면 더이상 HP를 감소시키지 않도록 조건 추가
        if ((HP - value) <= 0)
        {
            HP = 0;

            intHP = Mathf.FloorToInt(HP);
            SetActiveHPBar(intHP, false);

            DataManager.instance.nowPlayer.playerHP = intHP;
            DataManager.instance.SaveData(DataManager.instance.nowSlot);

            // 이미지 변경
            foreach (Image hpBar in HPBars)
            {
                hpBar.sprite = decreaseSprite; // 감소 이미지로 이미지 변경
            }

            if (GameOverEnding.instance != null) 
            {
                GameOverEnding.instance.GameOver();
            } 
            else 
            {
                Debug.Log("GameOver instance Null");
            }
            return;
        }

        switch (DataManager.instance.nowPlayer.nowCharacter)
        {
            case "Eden":
                if (DataManager.instance.nowPlayer.currentMap == "태초의숲")
                {
                    HP -= (value / 2);
                }
                else
                {
                    HP -= value;
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
                if (DataManager.instance.nowPlayer.currentMap == "파멸된도시")
                {
                    HP -= (value / 2);
                }
                else
                {
                    HP -= value;
                }
                break;
            case "None":
                HPDecreased = true;
                HP -= value;
                break;
        }
        
        intHP = Mathf.FloorToInt(HP);
        SetActiveHPBar(intHP, false);

        DataManager.instance.nowPlayer.playerHP = intHP;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);

        // 이미지 변경
        foreach (Image hpBar in HPBars)
        {
            hpBar.sprite = decreaseSprite; // 감소 이미지로 이미지 변경
        }

        // image 변경 후, 3초 후에 원래 이미지로 돌아가도록 코루틴 호출
        StartCoroutine(ChangeHPBarColor(decreaseSprite, originalSprite, intHP));
    }
    

    public void IncreaseHP(int value)
    {
        HPIncreased = true; 
        HP += value;

        // 현재 HP가 100을 초과하지 않도록 보정
        if (HP > 100)
        {
            HP = 100;
        }

        intHP = Mathf.FloorToInt(HP);
        SetActiveHPBar(intHP, true);
        
        DataManager.instance.nowPlayer.playerHP = intHP;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);

        // 이미지 변경
        foreach (Image hpBar in HPBars)
        {
            hpBar.sprite = increaseSprite; // 새로운 스프라이트로 이미지 변경
        }
        // image 변경 후, 3초 후에 원래 이미지로 돌아가도록 코루틴 호출
        StartCoroutine(ChangeHPBarColor(increaseSprite, originalSprite, intHP));
    }
    
    public void SetActiveHPBar(int _HP, bool isIncrease)
    {
        Sprite targetSprite = isIncrease ? increaseSprite : decreaseSprite;

        for (int i = 0; i < HPBars.Count; i++)
        {
            Color color = HPBars[i].color;

            if (i < _HP / 5)
                color.a = 1f;
            else
                color.a = 0f;

            HPBars[i].color = color;
        }

        // 이미지 변경
        foreach (Image hpBar in HPBars)
        {
            hpBar.sprite = targetSprite;
        }

        HP_Text.text = Mathf.FloorToInt(_HP).ToString();
        HP_Text2.text = Mathf.FloorToInt(_HP).ToString();
    }

    private IEnumerator ChangeHPBarColor(Sprite targetSprite, Sprite originalSprite, int _HP)
    {
        // 색상 변경 지연
        yield return new WaitForSeconds(colorChangeDelay);

        // 이미지를 원래 이미지로 복원
        foreach (Image hpBar in HPBars)
        {
            hpBar.sprite = originalSprite;
        }
    }
}