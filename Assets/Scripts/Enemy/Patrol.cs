using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [HideInInspector] public Node startingNode = null;
    Node targetNode = null;
    List<Node> travelPath = new List<Node>();
    int targetIndex = 0;
    [HideInInspector] public bool hasPath;
    Vector3 currentNodePosition;
    public override void SetBehaviour(AIManager aiManager)
    {
        aiManager.SetMovement(aiManager.roamingBehavior);
        if (DetectPlayer(player, plant, 10f))
        {
            startingNode = null;
            travelPath.Clear();
            hasPath = false;
            aiManager.SetMovement(aiManager.captureBehavior);
        }
        else
        {
            if (!hasPath)
            {
                GetPath();
            }
            else
            {
                FollowPath(aiManager);
                //hasPath = false;
            }
        }
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
        Vector3 angle1 = (plant.position - travelPath[0].Position).normalized;
        Vector3 angle2 = (plant.position - travelPath[1].Position).normalized;
        if (Vector3.Dot(angle1, plant.forward) < 0 && Vector3.Dot(angle2, plant.forward) > 0)
            travelPath.RemoveAt(0);
        targetIndex = 0;
        currentNodePosition = new Vector3(travelPath[targetIndex].Position.x, 
                                          1f, travelPath[targetIndex].Position.z);
        hasPath = true;
    }

    void FollowPath(AIManager aiManager)
    {
        plant.position = Vector3.MoveTowards(plant.position, currentNodePosition, 0.03f);
        plant.LookAt(currentNodePosition);
        if (plant.position.x == currentNodePosition.x && plant.position.z == currentNodePosition.z)
        {
            if (travelPath[targetIndex].EnterRoom)
            {
                aiManager.StartCoroutine("EnterRoom");
            }
            targetIndex++;
            if (targetIndex < travelPath.Count)
                currentNodePosition = new Vector3(travelPath[targetIndex].Position.x, 1f, travelPath[targetIndex].Position.z);
            else if (targetIndex >= travelPath.Count)
            {
                startingNode = targetNode;
                hasPath = false;
                travelPath.Clear();
            }
        }
    }
}
