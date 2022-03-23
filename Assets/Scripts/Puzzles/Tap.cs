using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tap : Puzzle
{
    public GameObject water;

    [SerializeField] Sprite filledBucketSprite;

    bool isWaterRunning;
    bool hasBucket;

    Item item;

    private void Start()
    {
        requiredItems.Add(PuzzleManager.ItemType.Bucket);

    }
    void Update()
    {
        if (hasBucket)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isWaterRunning = true;
                StartCoroutine(WaterSpray());
            }
        }
        
    }
    IEnumerator WaterSpray()
    {
        Instantiate(water);
        yield return new WaitForSeconds(2f);
        Destroy(water);
    }
    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
    }
    public void HasBucket()
    {
        hasBucket = CheckItems();
        if (hasBucket)
        {
            //item = PuzzleManager.itemsInInventory.Find(requiredItems[0].GetType);
            //item = PuzzleManager.itemsInInventory[0];
            item.type = PuzzleManager.ItemType.FilledBucket;
            item.inventoryImage = filledBucketSprite;
        }
                
    }
}
