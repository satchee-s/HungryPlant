using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Puzzle
{
    public DoorController controller;

    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
    }

    public void BarricadeDoor()
    {
        controller.canBeOpened = false;
        Debug.Log("Door cannot be opened");
        //run animation
    }
}
