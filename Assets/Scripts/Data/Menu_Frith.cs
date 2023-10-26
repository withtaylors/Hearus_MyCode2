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

    public GameObject eden;
    public GameObject noah;
    public GameObject adam;
    public GameObject jonah;
    public GameObject edenInfo;
    public GameObject noahInfo;
    public GameObject adamInfo;
    public GameObject jonahInfo;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        if(DataManager.instance.nowPlayer.nowCharacter == "None")
        {
            Text.SetActive(true);
            FrithMenuInfo.SetActive(false);

            eden.SetActive(false);
            noah.SetActive(false);
            adam.SetActive(false);
            jonah.SetActive(false);
            edenInfo.SetActive(false);
            noahInfo.SetActive(false);
            adamInfo.SetActive(false);
            jonahInfo.SetActive(false);
        }
        else
        {
            Text.SetActive(false);
            FrithMenuInfo.SetActive(true);
            DataManager.instance.SaveData(DataManager.instance.nowSlot);
            switch (DataManager.instance.nowPlayer.nowCharacter)
            {
                case "Eden":
                    eden.SetActive(true);
                    edenInfo.SetActive(true);
                    Debug.Log("Eden");
                    break;
                case "Noah":
                    noah.SetActive(true);
                    noahInfo.SetActive(true);
                    Debug.Log("Noah");
                    break;
                case "Adam":
                    adam.SetActive(true);
                    adamInfo.SetActive(true);
                    Debug.Log("Adam");
                    break;
                case "Jonah":
                    jonah.SetActive(true);
                    jonahInfo.SetActive(true);
                    Debug.Log("Jonah");
                    break;
            }
        }
    }

    public void SetVisible()
    {
        Text.SetActive(false);
        FrithMenuInfo.SetActive(true);
    }
}
