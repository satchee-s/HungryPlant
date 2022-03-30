using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Node playerNode, plantStartingNode = null;
    float nodeDistanceFromPlayer;
    Vector3 currentNode;
    List<Node> travelPath;
    int targetIndex = 0;
    [SerializeField] float playerDetectionRange;
    public override void SetBehaviour(AIManager aiManager)
    {
        aiManager.SetMovement(aiManager.chaseBehavior);
        if (DetectPlayer(player.transform, plant, 0.005f, 20f))
        {
            aiManager.SetMovement(aiManager.captureBehavior);
        }
        else if (DetectPlayer(player.transform, plant, playerDetectionRange))
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
        playerNode = pathfinding.FindClosestNode(player.transform.position, 15f);
        plantStartingNode = pathfinding.FindClosestNode(plant.position);
        pathfinding.FindPath(playerNode, plantStartingNode);
        travelPath = pathfinding.final;
        Vector3 angle1 = (plant.position - travelPath[0].Position).normalized;
        Vector3 angle2 = (plant.position - travelPath[1].Position).normalized;
        if (Vector3.Dot(angle1, plant.forward) < 0 && Vector3.Dot(angle2, plant.forward) > 0)
            travelPath.RemoveAt(0);
        currentNode = new Vector3(travelPath[0].Position.x,
                                  1f, travelPath[0].Position.z);
        targetIndex = 0;
    }

    void FollowPath()
    {
        plant.position = Vector3.MoveTowards(plant.position, currentNode, 0.8f);
        plant.LookAt(currentNode);
        if (plant.position.x == currentNode.x && plant.position.z == currentNode.z)
        {
            targetIndex++;
            if (targetIndex < travelPath.Count)
            {
                currentNode = new Vector3(travelPath[targetIndex].Position.x, 1f, travelPath[targetIndex].Position.z);
            }
            else if (targetIndex >= travelPath.Count)
            {
                playerNode = null;
                plantStartingNode = null;
                travelPath.Clear();
            }
        }
        nodeDistanceFromPlayer = Vector3.Distance(playerNode.Position, player.transform.position);
    }
}
