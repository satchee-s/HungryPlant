using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Node playerNode, plantStartingNode = null;
    float nodeDistanceFromPlayer;
    Vector3 currentNode;
    List<Node> travelPath;
    int targetIndex;
    public override void SetBehaviour()
    {
        aiManager.SetMovement(aiManager.chaseBehavior);
        if (DetectPlayer(player, plant, 1f)) {
            aiManager.SetMovement(aiManager.captureBehavior);
        }
        else if (DetectPlayer(player, plant, 10f))
        {
            if (playerNode == null || nodeDistanceFromPlayer > 15f)
            {
                CalculatePath();
            }
            else
            {
                FollowPath();
            }
        }
        else
        {
            playerNode = null;
            aiManager.SetMovement(aiManager.roamingBehavior);
        }
        //nodeDistanceFromPlayer = Vector3.Distance(playerNode.Position, player.position);
    }

    void CalculatePath()
    {
        playerNode = pathfinding.FindClosestNode(player.position, 15f);
        plantStartingNode = pathfinding.FindClosestNode(plant.position);
        pathfinding.FindPath(playerNode, plantStartingNode);
        travelPath = pathfinding.final;
        currentNode = new Vector3(travelPath[0].Position.x, 
                                  plant.position.y, travelPath[0].Position.z);
        targetIndex = 0;
        //Debug.Log("calculate path");
    }

    void FollowPath()
    {
        plant.position = Vector3.MoveTowards(plant.position, currentNode, 0.05f);
        plant.LookAt(currentNode);
        if (plant.position.x == currentNode.x && plant.position.z == currentNode.z)
        {
            targetIndex++;
            if (targetIndex < travelPath.Count)
            {
                currentNode = new Vector3(travelPath[targetIndex].Position.x, plant.position.y, travelPath[targetIndex].Position.z);
            }
            else if (targetIndex >= travelPath.Count)
            {
                playerNode = null;
                plantStartingNode = null;
                travelPath.Clear();
            }
        }
    }
}
