using UnityEngine;
using System.Collections.Generic;

public class Chase : State
{
    [SerializeField] float maxSpeed, frames, captureDistance, smooth, collisionDist;
    Vector3 finalVelocity = Vector3.zero;
    Vector3 desiredPos, desiredVelocity, currentNode;
    RaycastHit hit;
    
    public override void SetBehaviour(AIManager aiManager)
    {
        if (DetectPlayer(player.transform, transform, captureDistance))
        {
            aiManager.SetMovement(aiManager.captureBehavior);
        }
        else if (DetectPlayer(player, transform, playerDetectionDistance) || BehindPlant(player, transform))
        {
            FollowPath();
        }
        else
        {
            aiManager.SetMovement(aiManager.roamingBehavior);
        }
    }

    void FollowPath()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, collisionDist, ~playerLayer))
        {
            currentNode = pathfinding.FindClosestNode(player.position).Position;
        }
        else
        {
            currentNode = player.position;
        }

        desiredPos = currentNode;
        desiredPos = desiredPos + desiredVelocity;
        desiredVelocity = (transform.position - desiredPos).normalized * maxSpeed;
        finalVelocity = finalVelocity - desiredVelocity;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);
        transform.position += (finalVelocity) * Time.deltaTime;

        Vector3 rotationPos = (transform.position - currentNode).normalized * -1f;
        Quaternion desiredRotation = Quaternion.LookRotation(rotationPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smooth * Time.deltaTime);
    }
}
