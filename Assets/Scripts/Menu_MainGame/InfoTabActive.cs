using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTabActive : MonoBehaviour
{
    public GameObject InfoTab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InfoTab.SetActive(true);
        }
    }
}
