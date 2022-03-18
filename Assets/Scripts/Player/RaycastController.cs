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
    float timer = 0f;

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
        ItemController();
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "Puzzle")
                {
                    hit.collider.GetComponent<Puzzle>().ExecutePuzzle();
                }
                else if (hit.collider.tag == "Door")
                {
                    hit.collider.gameObject.GetComponent<DoorController>().PlayAnimation();
                }
            }
        }
    }

    private void ItemController()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.currentSlot.isFilled == true)
            {
                inventory.EmptySlot();

            }
            else
            {
                if (Physics.Raycast(ray, out hit, 4f))
                //if (Physics.SphereCast(ray, 2f, out hit))
                {
                    item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        inventory.SlotManager(item);
                    }
                }
            }
        }
    }
}
