using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Node targetNode;
    [HideInInspector] public Node startingNode;
    Vector3 currentNode;
    int targetIndex;

    [SerializeField] float maxSpeed, collisionDist, maxAvoid;
    [SerializeField] float smooth;
    Vector3 finalVelocity = Vector3.zero;
    Vector3 desiredPos;
    Vector3 desiredVelocity;

    bool hasPath = false;
    List<Node> travelPath = new List<Node>();
    [SerializeField] SearchRoom searchRoom;

	public Vector2 soundRange;
    float soundTimer;
    float timeLimit;

    RaycastHit hit;
    Vector3 avoidanceForce;

    public override void SetBehaviour(AIManager aiManager)
    {
        if (DetectPlayer(player, transform, playerDetectionDistance) || BehindPlant(player, transform))
        {
            ResetPath(null);
            aiManager.SetMovement(aiManager.chaseBehavior);
        }
        else
        {
            if (!hasPath)
            {
                CalculatePath();
            }
            else
            {
                FollowPath();
                SoundTiming(aiManager);
            }
        }
    }

    void FollowPath()
    {
        desiredPos = currentNode;
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

        Vector3 rotationPos = (transform.position - currentNode).normalized * -1f;
        Quaternion desiredRotation = Quaternion.LookRotation(rotationPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smooth * Time.deltaTime);

        if (PlantInRange(currentNode))
        {
            targetIndex++;
            if (targetIndex < travelPath.Count)
            {
                currentNode = new Vector3(travelPath[targetIndex].Position.x, 1f, travelPath[targetIndex].Position.z);
                //if (travelPath[targetIndex].EnterRoom && travelPath[targetIndex + 1].EnterRoom)
                //{
                //    searchRoom.PlayAnimation();
                //}
            }
            else if (targetIndex >= travelPath.Count)
            {
                ResetPath(targetNode);
            }
        }
    }

    void ResetPath(Node endNode)
    {
        startingNode = endNode;
        hasPath = false;
        travelPath.Clear();
    }

    void CalculatePath()
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
        if (Vector3.Dot(angle1, transform.forward) > 0 && Vector3.Dot(angle2, transform.forward) < 0)
            travelPath.RemoveAt(0);
        currentNode = new Vector3(travelPath[0].Position.x, 1f, travelPath[0].Position.z);
        targetIndex = 0;

        hasPath = true;
    }

    void SoundTiming(AIManager manager)
    {
        if (timeLimit == 0)
        {
            timeLimit = Random.Range(soundRange.x, soundRange.y);
        }

        if (soundTimer >= timeLimit)
        {
            manager.PlaySound();
            soundTimer = 0;
            timeLimit = Random.Range(soundRange.x, soundRange.y);
        }
        else
            soundTimer += Time.deltaTime;
    }
}
