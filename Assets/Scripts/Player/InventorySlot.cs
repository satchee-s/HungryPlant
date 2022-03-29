using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject UIBorder;
    public Image UIImage;
    public Transform slot;
    [HideInInspector] public Sprite imageInInventory;
    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isFilled;
    [HideInInspector] public Item item;

    Animator animator;

    private void Start()
    {
        animator = UIBorder.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void DeleteItem()
    {
        UIImage.sprite = null;
        item.gameObject.SetActive(true);
        Destroy(item.gameObject);
        item = null;
        isFilled = false;
    }

    public void AnimSelect(bool select)
    {
        animator.SetBool("Selected", select);
    }
}
