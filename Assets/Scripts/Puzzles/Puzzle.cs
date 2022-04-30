using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public List<PuzzleManager.ItemType> requiredItems = new List<PuzzleManager.ItemType>();
    public List<PuzzleManager.ItemType> consumeItems = new List<PuzzleManager.ItemType>();
    public UnityEvent taskCompleted;
    protected InventoryManager inventoryManager;
    protected SubtitleSystem subtitle;
    
    public InteractableLogic interactable;

    public bool CheckItems()
    {
        bool finalResult = true;
        string required = "";
        int itemCounter = 0;
        if (requiredItems.Count == 0)
            return true;
        for (int i = 0; i < requiredItems.Count; i++)
        {
            if (!PuzzleManager.itemsInInventory.Contains(requiredItems[i]))
            {
                if (itemCounter > 0)
                {
                    required = required + " and " + requiredItems[i].ToString();
                }
                else
                    required = required + requiredItems[i].ToString();
                itemCounter++;
                finalResult = false;                
            }
        }
        if (!finalResult)
            subtitle.DisplaySubtitle("I still need " + required + " for this");
        return finalResult;

    }
    private void Start()
    {
        inventoryManager = GameObject.Find("PlayerParent").GetComponent<InventoryManager>();
        subtitle = FindObjectOfType<SubtitleSystem>();
    }

    public virtual void ExecutePuzzle()
    {
        //CheckItems();
        if (CheckItems())
        {
            //Debug.Log("You have all the items");
            taskCompleted.Invoke();
            interactable.SetInteracting(false);
            for (int i = 0; i < consumeItems.Count; i++)
            {
                ConsumeItem(consumeItems[i]);
            }
        }
    }

    public void SwapItem(PuzzleManager.ItemType oldItem, PuzzleManager.ItemType newItem, Sprite newSprite)
    {
        
        for (int i = 0; i < inventoryManager.slots.Length; i++)
        {
            if (inventoryManager.slots[i].item.type == oldItem)
            {
                inventoryManager.slots[i].item.ChangeType(newItem);
                inventoryManager.ReplaceImage(inventoryManager.slots[i].UIImage.sprite, newSprite);
                inventoryManager.slots[i].item.ChangeSprite(newSprite);
                break;
            }         
        }
        for (int i = 0; i < PuzzleManager.itemsInInventory.Count; i++)
        {
            if (PuzzleManager.itemsInInventory[i] == oldItem)
                PuzzleManager.itemsInInventory[i] = newItem;
        }
    }

    public virtual void ConsumeItem(PuzzleManager.ItemType item)
    {
        Debug.Log("Trying to remove " + item);
        Debug.Log("There are " + inventoryManager.slots.Length + " slots in inventory");
        for (int i = 0; i < inventoryManager.slots.Length; i++)
        {
            if (inventoryManager.slots[i].item != null)
            {
                if (inventoryManager.slots[i].item.type == item)
                {
                    Debug.Log("Consuming Item");
                    inventoryManager.slots[i].DeleteItem();
                    PuzzleManager.itemsInInventory.Remove(item);
                    break;
                }
                else
                    Debug.Log(inventoryManager.slots[i].item.type + "not Consumed");
            }
            
        }
    }
}
