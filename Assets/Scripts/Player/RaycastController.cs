using UnityEngine;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    Item item;
    InventoryManager inventory;
    Ray ray;
    [SerializeField] LayerMask playerLayer;
    bool hitItem;
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
        if (Input.GetMouseButtonDown(0))
            ItemController();
        if (Input.GetKey(KeyCode.E))
            DropItem();
        if (Physics.Raycast(ray, out hit, 5f, ~playerLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "Door")
                {
                    //hit.collider.gameObject.GetComponent<DoorController>().PlayAnimation();
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
        if (Physics.Raycast(ray, out hit, 5f, ~playerLayer))
        {
            if (hit.collider.tag == "Item")
            {
                item = hit.transform.GetComponent<Item>();
                inventory.SlotManager(item);
            }
        }
    }
    private void DropItem()
    {
        if (inventory.currentSlot.isFilled == true)
        {
            inventory.EmptySlot();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, hit.point);
    }
}
