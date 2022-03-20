using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTask : Puzzle
{

    [SerializeField] bool startPuzzle = false;
    public DoorController controller;

    SubtitleSystem subtitle;

    // Start is called before the first frame update
    void Start()
    {
        subtitle = FindObjectOfType<SubtitleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LockDoor()
    {
        startPuzzle = true;
        subtitle.DisplaySubtitle("The Door has been locked");
        controller.Lock(true);
    }
}
