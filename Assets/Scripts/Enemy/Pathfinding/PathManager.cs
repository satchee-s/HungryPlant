using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    Pathfinding pathfinding;
    Node startingNode, targetNode;
    Vector3 currentNode;
    Transform plant;
    float movementSpeed;
    List<Node> currentPath = new List<Node>();

    /*private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
    }*/
    public void RequestPath (Node startNode, Node endNode)
    {
        //startingNode = startNode;
        //targetNode = endNode;
        currentPath.Clear();
        currentPath = pathfinding.final; 
    }
}
