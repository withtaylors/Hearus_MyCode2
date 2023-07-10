using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] ScriptEvent script;

    public Script[] GetScript()
    {
        script.scripts = DatabaseManager.instance.GetScript((int)script.line.x, (int)script.line.y);
        return script.scripts;
    }
}
