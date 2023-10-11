using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Frith : MonoBehaviour
{
    public static Menu_Frith instance;

    public GameObject Text; 

    public GameObject FrithMenuInfo;
    public GameObject RectImg;

    public GameObject edenInfo;
    public GameObject noahInfo;
    public GameObject adamInfo;
    public GameObject jonahInfo;

    public int frithIntimacy = 0;
    public TextMeshProUGUI frithIntimacy_Text;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        Debug.Log("Menu_Frith");
        if(DataManager.instance.nowPlayer.nowCharacter == "None")
        {
            Text.SetActive(true);
            RectImg.SetActive(false);
            FrithMenuInfo.SetActive(false);
            edenInfo.SetActive(false);
            noahInfo.SetActive(false);
            adamInfo.SetActive(false);
            jonahInfo.SetActive(false);
        }
        else
        {
            Text.SetActive(false);
            RectImg.SetActive(true);
            FrithMenuInfo.SetActive(true);
            DataManager.instance.SaveData(DataManager.instance.nowSlot);

            switch (DataManager.instance.nowPlayer.nowCharacter)
            {
                case "Eden":
                    edenInfo.SetActive(true);
                    Debug.Log("Eden");
                    break;
                case "Noah":
                    noahInfo.SetActive(true);
                    Debug.Log("Noah");
                    break;
                case "Adam":
                    adamInfo.SetActive(true);
                    Debug.Log("Adam");
                    break;
                case "Jonah":
                    jonahInfo.SetActive(true);
                    Debug.Log("Jonah");
                    break;
            }
        }
    }

    public void increaseIntimacy()
    {
        frithIntimacy += 10;

        SetFrithIntimacy(frithIntimacy);
        DataManager.instance.nowPlayer.frithIntimacy = frithIntimacy;
        DataManager.instance.SaveData(DataManager.instance.nowSlot);
    }

    public void SetFrithIntimacy(int _frithIntimacy)
    {


        frithIntimacy_Text.text = frithIntimacy.ToString();
    }
}
