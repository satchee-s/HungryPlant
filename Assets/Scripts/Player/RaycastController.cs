using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    Item item;
    InventoryManager inventory;
    Ray ray;
    //float timer = 0f;

    private void Start()
    {
        cam = Camera.main;
        item = null;
        inventory = FindObjectOfType<InventoryManager>();
    }
    private void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        inventory.SelectSlot();
        if (Input.GetKeyDown(KeyCode.E))
            ItemController();
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "Door")
                {
                    hit.collider.gameObject.GetComponent<DoorController>().PlayAnimation();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.tag == "Puzzle")
                {
                    hit.collider.GetComponent<Puzzle>().ExecutePuzzle();
                }
            }
        }
    }

    private void ItemController()
    {
        Debug.Log("attempting pickup");
        Debug.Log("slot is filled: " + inventory.currentSlot.isFilled);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Item")
            {
                Debug.Log("Picking up item");
                item = hit.transform.GetComponent<Item>();
                inventory.SlotManager(item);
            }
            else if (inventory.currentSlot.isFilled == true)
            {
                inventory.EmptySlot();
                Debug.Log("dropping item");
            }
        }
        
        Debug.Log("Ray hit tag: " + hit.collider.tag);
        
        //if (Input.GetKeyDown(KeyCode.E))
        //{            
        //    //else
        //    //{
        //    //    if (Physics.Raycast(ray, out hit))
        //    //    //if (Physics.SphereCast(ray, 2f, out hit))
        //    //    {                    
        //    //        if (hit.collider.tag == "Item")
        //    //        {
        //    //            Debug.Log("Picking up item");
        //    //            item = hit.transform.GetComponent<Item>();
        //    //            inventory.SlotManager(item);
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
