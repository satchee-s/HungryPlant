using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Node startingNode, targetNode;
    Vector3 currentNode;
    int targetIndex;
    [SerializeField] float maxSpeed;
    Vector3 finalVelocity = Vector3.zero;
    Vector3 desiredPos;
    Vector3 desiredVelocity;
    bool hasPath = false;
    List<Node> travelPath = new List<Node>();

    void FollowPath()
    {
        desiredPos = currentNode;
        desiredPos = desiredPos + desiredVelocity;
        desiredVelocity = (transform.position - desiredPos).normalized * maxSpeed;
        finalVelocity = finalVelocity - desiredVelocity;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);
        transform.position += finalVelocity * Time.deltaTime;
        transform.LookAt(currentNode);
        if (PlantInRange(currentNode))
        {
            targetIndex++;
            if (targetIndex < travelPath.Count)
                currentNode = new Vector3(travelPath[targetIndex].Position.x, 1f, travelPath[targetIndex].Position.z);
            else if (targetIndex >= travelPath.Count)
            {
                startingNode = targetNode;
                hasPath = false;
                travelPath.Clear();
            }
        }
    }

    public override void SetBehaviour(AIManager aiManager)
    {
        if (DetectPlayer(player, transform, playerDetectionDistance))
        {
            aiManager.SetMovement(aiManager.chaseBehavior);
        }
        else
        {
            //aiManager.SetMovement(aiManager.roamingBehavior);
            if (!hasPath)
            {
                CalculatePath();
            }
            else
            {
                FollowPath();
            }
        }
        
    }

    void CalculatePath()
    {
        if (startingNode == null)
        {
            startingNode = pathfinding.FindClosestNode(transform.position);
        }
        targetNode = pathfinding.GetRandomNode(startingNode);
        //GetPath(startingNode, targetNode, currentNode, travelPath);
        pathfinding.FindPath(targetNode, startingNode);
        travelPath = pathfinding.final;
        Vector3 angle1 = (transform.position - travelPath[0].Position).normalized;
        Vector3 angle2 = (transform.position - travelPath[1].Position).normalized;
        if (Vector3.Dot(angle1, transform.forward) < 0 && Vector3.Dot(angle2, transform.forward) > 0)
            travelPath.RemoveAt(0);
        currentNode = new Vector3(travelPath[0].Position.x,
                                  1f, travelPath[0].Position.z);
        targetIndex = 0;

        hasPath = true;
    }
}
