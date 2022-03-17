using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager
{
    public enum ItemType { Key, Nails, Planks, SteelBar, Hammer, Welder, Other };
    public static List<ItemType> itemsInInventory = new List<ItemType>();
}
