using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tap : Puzzle
{
    public GameObject water;
    public Transform waterPosition;
    [SerializeField] AudioSource faucetSound;
    [SerializeField] Sprite filledBucketSprite;
    [SerializeField] Sprite originalImage;

    public bool isBucketFilled;
    bool hasBucket;
    //public InventoryManager inventoryManager;
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            
        //    StartCoroutine(WaterSpray());
        //}

    }
    public IEnumerator WaterSpray()
    {
        GameObject particle = Instantiate(water, waterPosition) as GameObject;
        faucetSound.Play();
        yield return new WaitForSeconds(4f);
        Destroy(particle);
    }

    public void Water()
    {
        StartCoroutine(WaterSpray());
    }
    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();        
    }
    public void HasBucket()
    {
        SwapItem(PuzzleManager.ItemType.EmptyBucket, PuzzleManager.ItemType.Bucket, filledBucketSprite);
    }
}
