using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    public enum ItemType { Key, Nails, Planks, Hammer, Lighter, Gasoline, Bucket, FilledBucket, VideoTape, DrawerKey };
    public static List<ItemType> itemsInInventory = new List<ItemType>();
}
