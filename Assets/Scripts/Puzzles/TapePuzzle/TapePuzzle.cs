using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePuzzle : Puzzle
{
    public bool powered;

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
        if (CheckItems() && powered)
        {
            Debug.Log("You have all the items");
            taskCompleted.Invoke();
        }
        else
        {
            Debug.Log("You don't have all the items yet");
        }
    }
}
