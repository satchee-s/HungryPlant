using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Node currentNode;
    public Node[] allNodes;
    List <Node> open = new List <Node> ();
    [HideInInspector] public List <Node> final = new List <Node> ();
    LayerMask nodeLayer;
    private void Start()
    {
        allNodes = FindObjectsOfType<Node>();
        nodeLayer = LayerMask.GetMask("Nodes");
    }
    public Node FindClosestNode(Vector3 position, float searchDistance = 10f)
    {
        Node closestNode = null;
        Collider[] col = Physics.OverlapSphere(position, searchDistance, nodeLayer);
        float closestDistance = Mathf.Infinity;
        foreach (Collider nodes in col)
        {
            float distance = CalculateCost(nodes.transform.position, position);
            if (distance < closestDistance)
            {
                closestNode = nodes.GetComponent<Node>();
                closestDistance = distance;
            }
        }
        return closestNode;
    }
     
    float CalculateCost(Vector3 nodeA, Vector3 nodeB)
    {
        return Mathf.Abs(nodeA.x - nodeB.x) + Mathf.Abs(nodeA.z - nodeB.z);
    }
    public void FindPath(Node startingNode, Node targetNode)
    {
        if (startingNode == targetNode)
        {
            final.Add(startingNode);
        }
        else
        {
            bool containsInOpen;
            open.Add(startingNode);

            while (true)
            {
                open.Sort();
                currentNode = open[0];
                open.Remove(currentNode);
                currentNode.isVisited = true;
                if (currentNode == targetNode)
                {
                    FinalPath(targetNode, startingNode);
                    break;
                }
                for (int i = 0; i < currentNode.neighbours.Count; i++)
                {
                    containsInOpen = open.Contains(currentNode.neighbours[i]);
                    if (currentNode.neighbours[i].isVisited == true)
                        continue;
                    currentNode.neighbours[i].gCost = (int)CalculateCost(currentNode.neighbours[i].Position, startingNode.Position);
                    currentNode.neighbours[i].hCost = (int)CalculateCost(currentNode.neighbours[i].Position, targetNode.Position);
                    if (currentNode.neighbours[i].hCost < currentNode.hCost || !containsInOpen)
                    {
                        currentNode.neighbours[i].parent = currentNode;
                        if (!containsInOpen)
                            open.Add(currentNode.neighbours[i]);
                    }
                }
            }
        }
    }

    void FinalPath(Node endNode, Node startNode)
    {
        final.Add(endNode);
        while (true)
        {
            final.Add(endNode.parent);
            endNode = final[final.Count - 1];
            if (final[final.Count - 1] == startNode)
            {
                break;
            }
        }
        ClearPath();
    }

    public void ClearPath()
    {
        //final.Clear();
        open.Clear();
        foreach (Node node in allNodes)
            node.isVisited = false;
    }

    public Node GetRandomNode(Node node)
    {
        Node newNode = allNodes[Random.Range(0, allNodes.Length - 1)];
        while (true)
        {
            if (node != newNode && !node.neighbours.Contains(newNode))
                return newNode;
            else
                newNode = allNodes[Random.Range(0, allNodes.Length - 1)];
        }
    }
}
