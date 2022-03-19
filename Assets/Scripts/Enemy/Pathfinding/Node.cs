using UnityEngine;
using System;
using System.Collections.Generic;

public class Node : MonoBehaviour, IComparable
{
    public List<Node> neighbours;
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public int fCost { get { return gCost + hCost; } }
    [HideInInspector] public Node parent;
    [HideInInspector] public bool isVisited;
    public Vector3 Position { get { return transform.position; } }
    public int CompareTo(object obj)
    {
        Node node = obj as Node;
        if (node.fCost > fCost)
            return -1;
        else if (node.fCost < fCost)
            return 1;
        return 0;
    }
}
