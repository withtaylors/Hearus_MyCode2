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

    // 프리스 정보 잠금
    public List<GameObject> lockedInformation;
    // 프리스 정보 열림
    public List<GameObject> openInformation;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // 프리스 정보 메뉴, 프리스 캐릭터 visability
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
                    break;
                case "Noah":
                    noah.SetActive(true);
                    noahInfo.SetActive(true);
                    break;
                case "Adam":
                    adam.SetActive(true);
                    adamInfo.SetActive(true);
                    break;
                case "Jonah":
                    jonah.SetActive(true);
                    jonahInfo.SetActive(true);
                    break;
            }
        }

        UnlockFrithInformation(); // 프리스 정보 잠금 해제
    }
    public void SetVisible()
    {
        Text.SetActive(false);
        FrithMenuInfo.SetActive(true);
    }

    public void UnlockFrithInformation()
    {
        Debug.Log(" Inside UnlockFrithInformation");

        int unlockCount = DataManager.instance.nowPlayer.FrithInfo / 2;

        for (int i = 0; i < lockedInformation.Count; i++)
        {
            if (unlockCount > 0 && lockedInformation[i])
            {
                lockedInformation[i].SetActive(false);
                openInformation[i].SetActive(true);
                unlockCount--;
            }
        }  
    }
}
