using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform[] inventory = new Transform[3];
    InventorySlot[] slots = new InventorySlot[3];
    public InventorySlot currentSlot;
    //public List<PuzzleManager.ItemType> itemsInInventory = new List<PuzzleManager.ItemType>();

    private void Start()
    {
        slots[0] = inventory[0].GetComponent<InventorySlot>();
        slots[1] = inventory[1].GetComponent<InventorySlot>();
        slots[2] = inventory[2].GetComponent<InventorySlot>();
        currentSlot = slots[0];
    }
    InventorySlot FindEmpty()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isFilled == false)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectSlot() //called in update
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("Slot 1 selected");
            currentSlot = slots[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("Slot 2 selected");
            currentSlot = slots[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Debug.Log("Slot 3 selected");
            currentSlot = slots[2];
        }
    }

    void FillSlot(Item item)
    {
        currentSlot = FindEmpty();
        if (currentSlot != null)
        {
            currentSlot.item = item;
            currentSlot.isFilled = true;
            item.transform.SetParent(currentSlot.slot, false);
            currentSlot.UIImage.sprite = item.inventoryImage;
            item.gameObject.SetActive(false);
            PuzzleManager.itemsInInventory.Add(item.type);
        }
    }

    public void EmptySlot()
    {
        currentSlot.UIImage.sprite = null;
        currentSlot.item.transform.SetParent(null, false);
        currentSlot.item.transform.position = currentSlot.slot.position;
        currentSlot.item.transform.rotation = currentSlot.slot.rotation;
        currentSlot.isFilled = false;
        currentSlot.item.gameObject.SetActive(true);
        currentSlot.item.rbd.AddForce(currentSlot.item.transform.forward * 2, ForceMode.VelocityChange);
        PuzzleManager.itemsInInventory.Remove(currentSlot.item.type);
        currentSlot.item = null;
        //Debug.Log("Slot emptied");
    }

    public void SlotManager(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isFilled == false)
            {
                FillSlot(item);
                break;
            }
        }

    }
}
