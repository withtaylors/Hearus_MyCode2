using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSwitcher : MonoBehaviour
{
    public GameObject[] imageObjects;
    private int currentIndex = 0;

    private void Start()
    {
        UpdateImage();
    }
    
    public void SwitchImage(bool isRight)
    {
        // 비활성화 현재 이미지 오브젝트
        imageObjects[currentIndex].SetActive(false);

        if (isRight)
        {
            currentIndex = (currentIndex + 1) % imageObjects.Length;
        }
        else
        {
            currentIndex = (currentIndex - 1 + imageObjects.Length) % imageObjects.Length;
        }

        // 활성화 새로운 이미지 오브젝트
        imageObjects[currentIndex].SetActive(true);
    }

    private void UpdateImage()
    {
        // 초기 상태에서는 첫 번째 이미지만 활성화
        for (int i = 0; i < imageObjects.Length; i++)
        {
            imageObjects[i].SetActive(i == currentIndex);
        }
    }
}
