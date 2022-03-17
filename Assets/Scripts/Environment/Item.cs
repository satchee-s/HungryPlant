using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Rigidbody rbd;
    public Sprite inventoryImage;

    //public enum ItemType {Key, Nails, Planks, SteelBar, Hammer, Welder}
    public PuzzleManager.ItemType type;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }

}
