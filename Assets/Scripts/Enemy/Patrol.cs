using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Node startingNode, endNode, targetNode;
    Vector3 currentNode;
    int targetIndex;
    [SerializeField] float maxForce, maxSpeed, frames;
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
        if (PlantInRange())
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

    bool PlantInRange()
    {
        float range = 0.2f;
        if (Mathf.Abs(transform.position.x - currentNode.x) <= range && Mathf.Abs(transform.position.z - currentNode.z) <= range)
            return true;
        return false;
    }

    public override void SetBehaviour(AIManager aiManager)
    {
        aiManager.SetMovement(aiManager.roamingBehavior);
        if (!hasPath)
        {
            GetPath();
        }
        else
        {
            FollowPath();
        }
    }

    void GetPath()
    {
        if (startingNode == null)
        {
            startingNode = pathfinding.FindClosestNode(transform.position);
        }
        targetNode = pathfinding.GetRandomNode(startingNode);
        pathfinding.FindPath(targetNode, startingNode);
        travelPath = pathfinding.final;
        Vector3 angle1 = (transform.position - travelPath[0].Position).normalized;
        Vector3 angle2 = (transform.position - travelPath[1].Position).normalized;
        if (Vector3.Dot(angle1, transform.forward) < 0 && Vector3.Dot(angle2, transform.forward) > 0)
            travelPath.RemoveAt(0);
        targetIndex = 0;
        currentNode = new Vector3(travelPath[targetIndex].Position.x,
                                  1f, travelPath[targetIndex].Position.z);
        hasPath = true;
    }

}
