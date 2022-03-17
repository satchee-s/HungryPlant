using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    public enum PuzzleState { NotStarted, InProgress, Completed};
    //public List<string> finishedPuzzles = new List<string>();
    public List<GameObject> requiredItems = new List<GameObject>();
    public List<GameObject> usedItems = new List<GameObject>();
}
