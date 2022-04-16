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
    public bool EnterRoom;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Position, 0.5f);
        Gizmos.color = Color.red;
        for (int i = 0; i < neighbours.Count; i++)
        {
            Gizmos.DrawLine(Position, neighbours[i].Position);
        }
    }
}
