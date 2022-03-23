using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Node startingNode = null;
    Node targetNode = null;
    List<Node> travelPath = new List<Node>();
    int targetIndex = 0;
    bool hasPath;
    Vector3 currentNode;
    public override void SetBehaviour()
    {
        aiManager.SetMovement(aiManager.roamingBehavior);
        PathManager();
    }

    void GetPath()
    {
        if (startingNode == null)
        {
            startingNode = pathfinding.FindClosestNode(plant.position);
        }
        targetNode = pathfinding.GetRandomNode(startingNode);
        pathfinding.FindPath(targetNode, startingNode);
        travelPath = pathfinding.final;
        targetIndex = 0;
        currentNode = travelPath[targetIndex].Position;
        hasPath = true;
        //FollowPath();
    }

    void FollowPath()
    {
        //targetIndex = 0;
        //currentNode = travelPath[targetIndex].Position;
        plant.position = Vector3.MoveTowards(plant.position, currentNode, 0.1f);
        if (plant.position == currentNode)
        {
            targetIndex++;
            if (targetIndex < travelPath.Count)
                currentNode = travelPath[targetIndex].Position;
            else if (targetIndex >= travelPath.Count)
            {
                startingNode = targetNode;
                hasPath = false;
                travelPath.Clear();
                //break;
            }
        }
        //travelPath.Clear();
    }

    void PathManager()
    {
        if (!hasPath)
        {
            GetPath();
        }
        else
        {
            FollowPath();
        }
    }
}
