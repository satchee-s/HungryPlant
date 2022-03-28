using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTask : Puzzle
{

    [SerializeField] bool startPuzzle = false;
    public DoorController controller;

    public void LockDoor()
    {
        startPuzzle = true;
        subtitle.DisplaySubtitle("The Door has been locked", 5, .1f);
        if (controller != null)
            controller.Lock(true);
    }
}
