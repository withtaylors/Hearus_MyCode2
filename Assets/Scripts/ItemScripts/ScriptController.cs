using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour
{
    public GameObject go_DialoguePanel;

    void Start()
    {
        go_DialoguePanel.SetActive(false);
    }

    public void isFindItem()
    {
        go_DialoguePanel.SetActive(true);
    }
}
