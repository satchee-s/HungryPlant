using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Vector3 currentNode;

    [SerializeField] float maxSpeed, frames, captureDistance;
    Vector3 finalVelocity = Vector3.zero;
    Vector3 desiredPos;
    Vector3 desiredVelocity;
    public override void SetBehaviour(AIManager aiManager)
    {
        if (DetectPlayer(player.transform, transform, captureDistance))
        {
            aiManager.SetMovement(aiManager.captureBehavior);

        }
        if (DetectPlayer(player.transform, transform, playerDetectionDistance))
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
        currentNode = player.position;
        desiredPos = currentNode;
        desiredPos = desiredPos + desiredVelocity;
        desiredVelocity = (transform.position - desiredPos).normalized * maxSpeed;
        finalVelocity = finalVelocity - desiredVelocity;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);
        transform.position += finalVelocity * Time.deltaTime;
        transform.LookAt(currentNode);


    }
}
