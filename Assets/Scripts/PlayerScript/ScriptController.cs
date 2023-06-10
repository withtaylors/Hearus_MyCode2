using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class ScriptController : MonoBehaviour
{
    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private GameObject OptionPrefab;

    OptionsListView optionsListView;

    LineView lineView;

    private void FixedUpdate()
    {
        //if (OptionPrefab != null)
        //    continueButton.SetActive(false);
    }

    public void StartDialogue()
    {

    }

    public void EndDialogue()
    {
        continueButton.SetActive(false);
    }

    public void IsViewingOption()
    {

    }
}
