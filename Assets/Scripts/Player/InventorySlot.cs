using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject UIBorder;
    public Image UIImage;
    public Transform slot;
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
        if (animator.GetBool("Selected") != isSelected)
        {
            animator.SetBool("Selected", isSelected);
        }
    }
}
