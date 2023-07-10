using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptManager : MonoBehaviour
{
    [SerializeField] private GameObject go_ScriptPanel;
    [SerializeField] private TextMeshProUGUI scriptText;

    [SerializeField] ScriptEvent script;

    Script[] scripts;

    bool isDialogue = false;

    public void ShowScript(Script[] p_scripts)
    {
        scriptText.text = "";
        SettingUI(true);
        scripts = p_scripts;
    }

    private void SettingUI(bool p_flag)
    {
        go_ScriptPanel.SetActive(p_flag);
    }

    public Script[] GetScript()
    {
        script.scripts = DatabaseManager.instance.GetScript((int)script.line.x, (int)script.line.y);
        return script.scripts;
    }
}
