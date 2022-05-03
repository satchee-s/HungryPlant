using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform[] inventory = new Transform[3];
    public InventorySlot[] slots = new InventorySlot[3];
    public InventorySlot currentSlot;

    private void Start()
    {
        slots[0] = inventory[0].GetComponent<InventorySlot>();
        slots[1] = inventory[1].GetComponent<InventorySlot>();
        slots[2] = inventory[2].GetComponent<InventorySlot>();
        currentSlot = slots[0];
    }

    public void SelectSlot() //called in update
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlot = slots[0];
            currentSlot.isSelected = true;
            currentSlot.AnimSelect(true);
            slots[1].isSelected = false;
            slots[1].AnimSelect(false);
            slots[2].isSelected = false;
            slots[2].AnimSelect(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlot = slots[1];
            currentSlot.isSelected = true;
            currentSlot.AnimSelect(true);
            slots[0].isSelected = false;
            slots[0].AnimSelect(false);
            slots[2].isSelected = false;
            slots[2].AnimSelect(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlot = slots[2];
            currentSlot.isSelected = true;
            currentSlot.AnimSelect(true);
            slots[0].isSelected = false;
            slots[0].AnimSelect(false);
            slots[1].isSelected = false;
            slots[1].AnimSelect(false);
        }
    }

    void FillSlot(Item item, int slotIndex)
    {
        currentSlot = slots[slotIndex];
        if (currentSlot != null)
        {
            item.PickUpSubtitle();
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
        currentSlot.UIImage.sprite = currentSlot.empty;
        currentSlot.item.transform.SetParent(null, true);
        currentSlot.item.transform.position = currentSlot.slot.position;
        currentSlot.item.transform.rotation = currentSlot.slot.rotation;
        currentSlot.isFilled = false;
        currentSlot.item.gameObject.SetActive(true);
        //currentSlot.item.rbd.AddForce(currentSlot.item.transform.forward * 0.8f, ForceMode.VelocityChange);
        PuzzleManager.itemsInInventory.Remove(currentSlot.item.type);
        currentSlot.item = null;
    }

    public void SlotManager(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isFilled == false)
            {
                FillSlot(item, i);
                break;
            }
        }
    }

    public void ReplaceImage(Sprite inventoryImage, Sprite replacementImage)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].UIImage.sprite == inventoryImage)
            {
                slots[i].UIImage.sprite = replacementImage;
                break;
            }
        }
    }
}
