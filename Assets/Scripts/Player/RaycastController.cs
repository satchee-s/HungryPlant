using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    Item item;
    InventoryManager inventory;
    Ray ray;

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
                if (hit.collider.tag == "Door")
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
                if (Physics.Raycast(ray, out hit, 2f))
                //if (Physics.SphereCast(ray, 2f, out hit))
                {
                    item = hit.transform.GetComponent<Item>();
                    if (item != null)
                    {
                        Debug.Log("Item detected");
                        inventory.SlotManager(item);
                    }
                }
            }
        }
    }
}
