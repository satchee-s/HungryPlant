using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Rigidbody rbd;
    public Sprite inventoryImage;
    public PuzzleManager.ItemType type;

    InventoryManager inventory;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        inventory = FindObjectOfType<InventoryManager>();
        Light light = gameObject.AddComponent<Light>();
        light.intensity = .2f;
        light.range = 1;
        light.color = GetComponent<Renderer>().material.color;
    }
}
