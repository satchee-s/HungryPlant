using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePuzzle : Puzzle
{
    public bool powered;
    public SubtitleTrigger unpluggedText;

    private void Start()
    {
        powered = false;
        inventoryManager = GameObject.Find("PlayerParent").GetComponent<InventoryManager>();
    }

    public void PowerPlayer()
    {
        powered = true;
    }

    override public void ExecutePuzzle()
    {
        //CheckItems();
        if (CheckItems())
        {
            Debug.Log("You have all the items");
            if (powered)
            {
                base.ExecutePuzzle();
                taskCompleted.Invoke();
            }
            else
                unpluggedText.TriggerSubtitle();
        }
    }
}
