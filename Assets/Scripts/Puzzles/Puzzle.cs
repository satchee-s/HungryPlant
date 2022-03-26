using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public List<PuzzleManager.ItemType> requiredItems = new List<PuzzleManager.ItemType>();
    public UnityEvent taskCompleted;
    protected InventoryManager inventoryManager;
    protected SubtitleSystem subtitle;
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
    private void Start()
    {
        inventoryManager = GameObject.Find("PlayerParent").GetComponent<InventoryManager>();
        subtitle = FindObjectOfType<SubtitleSystem>();
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
            subtitle.DisplaySubtitle("You don't have all the items yet");
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
