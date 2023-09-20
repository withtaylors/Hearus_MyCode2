using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu_Frith : MonoBehaviour
{
    public GameObject Text; 

    public GameObject FrithMenuInfo;
    public GameObject RectImg;

    public GameObject edenInfo;
    public GameObject noahInfo;
    public GameObject adamInfo;
    public GameObject jonahInfo;

    public void Update()
    {
        if (DataManager.instance != null)
        {
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
                        break;
                    case "Noah":
                        noahInfo.SetActive(true);
                        break;
                    case "Adam":
                        adamInfo.SetActive(true);
                        break;
                    case "Jonah":
                        jonahInfo.SetActive(true);
                        break;
                }
            }
        }
        else
        {
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
                        break;
                    case "Noah":
                        noahInfo.SetActive(true);
                        break;
                    case "Adam":
                        adamInfo.SetActive(true);
                        break;
                    case "Jonah":
                        jonahInfo.SetActive(true);
                        break;
                }
            }
        }
    }
}
