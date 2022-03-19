using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Puzzle
{
    public DoorController controller;
    [SerializeField] float requriedTime;
    float timer = 0f;
    [SerializeField] bool startPuzzle = false;
    //add buffer/way to check how long you do the task for

    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
    }

    public void BarricadeDoor()
    {
        startPuzzle = true;

        controller.isNotBarricaded = false;
        Debug.Log("Door has been barricaded");
        //run animation
    }

    private void Update()
    {
        if (startPuzzle)
        {
            if (Input.GetMouseButton(1))
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            if (timer >= requriedTime)
            {
                Debug.Log("Pressed button for enough time");
                startPuzzle = false;
            }
        }
        
    }
}
