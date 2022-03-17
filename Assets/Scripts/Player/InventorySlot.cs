using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image UIImage;
    public Transform slot;
    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isFilled;
    [HideInInspector] public Item item;
}
