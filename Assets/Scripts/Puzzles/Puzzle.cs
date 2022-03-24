using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public List<PuzzleManager.ItemType> requiredItems = new List<PuzzleManager.ItemType>();
    public UnityEvent taskCompleted;    

    public bool CheckItems()
    {
        for (int i = 0; i < requiredItems.Count; i++)
        {
            if (!PuzzleManager.itemsInInventory.Contains(requiredItems[i]))
            {
                return false; 
            }
        }
        return true;
    }

    public virtual void ExecutePuzzle()
    {
        CheckItems();
        if (CheckItems())
        {
            //Debug.Log("You have all the items");
            taskCompleted.Invoke();
        }
        else
        {
            //Debug.Log("You don't have all the items yet");
        }
    }

    public void SwapItem(PuzzleManager.ItemType oldItem, PuzzleManager.ItemType newItem)
    {
        for (int i = 0; i < PuzzleManager.itemsInInventory.Count; i++)
        {
            if (PuzzleManager.itemsInInventory[i] == oldItem)
            {
                PuzzleManager.itemsInInventory[i] = newItem;
                break;
            }         
        }
    }
}
