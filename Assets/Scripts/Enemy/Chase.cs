using UnityEngine;

public class Chase : State
{
    [SerializeField] float maxSpeed, frames, captureDistance, smooth, collisionDist, maxAvoid;
    Vector3 finalVelocity = Vector3.zero;
    Vector3 desiredPos;
    Vector3 desiredVelocity;
    RaycastHit hit;
    Vector3 avoidanceForce;
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
        desiredPos = player.position;
        desiredPos = desiredPos + desiredVelocity;
        desiredVelocity = (transform.position - desiredPos).normalized * maxSpeed;
        finalVelocity = finalVelocity - desiredVelocity;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);

        if (Physics.Raycast(transform.position, transform.forward, out hit, collisionDist, ~playerLayer))
        {
            avoidanceForce = transform.position + finalVelocity;
            avoidanceForce = avoidanceForce - hit.point;
            avoidanceForce = Vector3.ClampMagnitude(avoidanceForce, maxAvoid);
            avoidanceForce.Normalize();
        }
        else
            avoidanceForce = Vector3.zero;

        transform.position += (finalVelocity + avoidanceForce) * Time.deltaTime;

        transform.position += (finalVelocity) * Time.deltaTime;

        Vector3 rotationPos = (transform.position - player.position).normalized * -1f;
        Quaternion desiredRotation = Quaternion.LookRotation(rotationPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smooth * Time.deltaTime);
    }
}
