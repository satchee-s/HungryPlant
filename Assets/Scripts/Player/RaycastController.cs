using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    Item item;
    InventoryManager inventory;
    Ray ray;
    public float rayDist;
    public float interactionDist;
    [SerializeField] LayerMask playerLayer;

    public Image reticle;
    public Sprite interactable;
    public Sprite normalIcon;
    public Vector2 minMaxReticleScale;
    Vector3 minScale;
    Vector3 maxScale;
    bool hitItem;
    bool hitDoor;
    bool hitPuzzle;
    bool hitEnvironmental;

    private void Start()
    {
        cam = Camera.main;
        item = null;
        inventory = FindObjectOfType<InventoryManager>();

        minScale = reticle.transform.localScale * minMaxReticleScale.x;
        maxScale = reticle.transform.localScale * minMaxReticleScale.y;
    }
    private void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.forward);
        inventory.SelectSlot();

        if (Physics.Raycast(ray, out hit, rayDist, ~playerLayer))
        {
            float distance = hit.distance;

            if (hit.collider.tag == "Door" && distance <= interactionDist) 
            {
                hitDoor = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("OpenDoor");
                    if (hit.collider.gameObject.GetComponent<DoorControl>() != null)
                        hit.collider.gameObject.GetComponent<DoorControl>().Interact();
                    else if (hit.collider.gameObject.GetComponent<Drawer_Pull_X>() != null)
                        hit.collider.gameObject.GetComponent<Drawer_Pull_X>().Interact();
                }                
            }
            else
            {
                hitDoor = false;
            }

            if (hit.collider.tag == "Puzzle" && distance <= interactionDist) 
            {
                hitPuzzle = true;
                if (hit.collider.GetComponent<Barricade>() != null)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        hit.collider.GetComponent<Puzzle>().ExecutePuzzle();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<Puzzle>().ExecutePuzzle();
                    }
                }                               
            }
            else
            {
                hitPuzzle = false;
            }

            if (hit.collider.tag == "Item" && distance <= interactionDist)
            {
                hitItem = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    item = hit.transform.GetComponent<Item>();
                    inventory.SlotManager(item);
                }                    
            }
            else
            {
                hitItem = false;
            }

            if (hit.collider.tag == "Environmental" && distance <= interactionDist)
            {
                hitEnvironmental = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<EnvironmentalComment>().TriggerComment();
                }
            }
            else
            {
                hitEnvironmental = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && !hitDoor && !hitPuzzle && !hitItem && !hitEnvironmental)
            {
                DropItem();
            }

            if (hitDoor || hitPuzzle || hitItem || hitEnvironmental)
            {
                reticle.sprite = interactable;
                reticle.transform.localScale = Vector3.Lerp(reticle.transform.localScale, maxScale, Time.deltaTime);
            }
            else
            {
                reticle.sprite = normalIcon;
                reticle.transform.localScale = Vector3.Lerp(reticle.transform.localScale, minScale, Time.deltaTime);
            }
        }
    }

    private void DropItem()
    {
        if (inventory.currentSlot.isFilled == true)
        {
            if (!Physics.Raycast(transform.position, transform.forward, 2f, ~playerLayer))
            {
                inventory.EmptySlot();
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        if (hitDoor)
            Gizmos.color = Color.yellow;
        else if (hitPuzzle)
            Gizmos.color = Color.blue;
        else if (hitItem)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        if (hit.distance < rayDist)
            Gizmos.DrawLine(transform.position, hit.point);
    }*/
}
