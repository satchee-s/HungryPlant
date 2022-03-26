using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Node currentNode;
    public Node[] allNodes;
    List <Node> open = new List <Node> ();
    [HideInInspector] public List <Node> final = new List <Node> ();
    private void Start()
    {
        allNodes = GameObject.FindObjectsOfType<Node>();
    }
    public Node FindClosestNode(Vector3 position) //raycast to find closest node instead?
    {
        Node closestNode = null;
        float closestDistance = Mathf.Infinity;
        foreach (Node node in allNodes)
        {
            float distance = CalculateCost(node.Position, position);

            if (distance < closestDistance)
            {
                closestNode = node;
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

    void FinalPath(Node endNode, Node startNode)
    {
        final.Add(endNode);
        while (true)
        {
            final.Add(endNode.parent);
            endNode = final[final.Count - 1];
            if (final[final.Count - 1] == startNode)
            {
                //enemy.gameObject.GetComponent<AIMovement>().OnPathFound(final);
                //Debug.Log("Found path");
                break;
            }
        }
        //final.Reverse();
        /*for (int i = 0; i < final.Count; i++)
        {
            Debug.Log(final[i].name);
        }*/
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
