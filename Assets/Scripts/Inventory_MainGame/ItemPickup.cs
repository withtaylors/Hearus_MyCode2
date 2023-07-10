using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ItemPickup : MonoBehaviour
{
    // E 키를 눌러 아이템을 습득함

    public int _itemID;
    public int _count;

    public DialogueRunner dialogue;

    ScriptManager scriptManager;

    private void Start()
    {
        //dialogue = gameObject.GetComponent<DialogueRunner>();
        scriptManager = FindObjectOfType<ScriptManager>();
    }

    public void Pickup(GameObject item, DialogueRunner dialogue)
    {
        int pickingID;
        int pickingCount;

        pickingID = item.gameObject.GetComponent<ItemPickup>()._itemID;
        pickingCount = item.gameObject.GetComponent<ItemPickup>()._count;

        //dialogue.StartDialogue(pickingID.ToString());

        TextLogs.instance.GetItemLog(pickingID);

        scriptManager.ShowScript(scriptManager.GetScript());
        Inventory.instance.GetAnItem(pickingID, pickingCount);
    }
}