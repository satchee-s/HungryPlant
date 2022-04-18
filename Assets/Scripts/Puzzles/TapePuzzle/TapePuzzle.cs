using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePuzzle : Puzzle
{
    public bool powered;
    public string unpluggedText;

    private void Start()
    {
        powered = false;
    }

    public void PowerPlayer()
    {
        powered = true;
    }

    override public void ExecutePuzzle()
    {
        CheckItems();
        if (CheckItems())
        {
            Debug.Log("You have all the items");
            if (powered)
                taskCompleted.Invoke();
            else
                subtitle.DisplaySubtitle(unpluggedText);
        }
        else
        {
            Debug.Log("You don't have all the items yet");
        }
    }
}
