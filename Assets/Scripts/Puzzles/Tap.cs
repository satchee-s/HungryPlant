using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tap : Puzzle
{
    public GameObject water;

    [SerializeField] Sprite filledBucketSprite;
    [SerializeField] Sprite originalImage;

    bool isWaterRunning;
    bool hasBucket;
    //public InventoryManager inventoryManager;
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
        SwapItem(PuzzleManager.ItemType.Bucket, PuzzleManager.ItemType.FilledBucket);
        inventoryManager.ReplaceImage(originalImage, filledBucketSprite);
    }
}
