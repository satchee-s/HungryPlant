using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public List<PuzzleManager.ItemType> requiredItems = new List<PuzzleManager.ItemType>();
    public UnityEvent taskCompleted;

    //InventoryManager manager;

    /*private void Start()
    {
        manager = GameObject.Find("PlayerParent").GetComponent<InventoryManager>();
    }*/

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
            Debug.Log("You have all the items");
            taskCompleted.Invoke();
        }
        else
        {
            Debug.Log("You don't have all the items yet");
        }
    }
}
