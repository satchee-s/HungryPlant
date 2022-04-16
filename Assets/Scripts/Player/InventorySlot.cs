using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject UIBorder;
    public Image UIImage;
    public Sprite empty;
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

    public void DeleteItem()
    {
        //UIImage.sprite = empty;
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
