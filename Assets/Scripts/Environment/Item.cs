using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Item : MonoBehaviour
{
    public Rigidbody rbd;
    public Sprite inventoryImage;
    public PuzzleManager.ItemType type;

    SubtitleSystem subtitleSystem;
    public string subtitle;
    bool triggered;

    public UnityEvent extras;

    InventoryManager inventory;

    void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        rbd = GetComponent<Rigidbody>();
        inventory = FindObjectOfType<InventoryManager>();
        Light light = gameObject.AddComponent<Light>();
        light.intensity = .2f;
        light.range = 1;
        light.color = GetComponent<Renderer>().material.color;
    }

    public void ChangeType(PuzzleManager.ItemType newType)
    {
        type = newType;
    }

    public void ChangeSprite(Sprite newSprite)
    {
        inventoryImage = newSprite;
    }

    public void PickUpSubtitle()
    {
        if (!triggered)
        {
            subtitleSystem.DisplaySubtitle(subtitle);
            extras.Invoke();
            triggered = true;
        }
    }
}
