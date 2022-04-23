using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [SerializeField] ComputerPuzzle puzzle;

    private void OnTriggerEnter(Collider other)
    {
        if (puzzle.gameCompleted)
        {
            //play good ending
        }
        else
        {
            //play bad ending
        }
    }
}
