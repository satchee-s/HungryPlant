using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    Node startingNode, targetNode;
    Vector3 currentNode;
    int targetIndex;

    [SerializeField] float maxSpeed;
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

    void FollowPath()
    {
        desiredPos = currentNode;
        desiredPos = desiredPos + desiredVelocity;
        desiredVelocity = (transform.position - desiredPos).normalized * maxSpeed;
        finalVelocity = finalVelocity - desiredVelocity;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);
        transform.position += finalVelocity * Time.deltaTime;

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
            aiManager.PlayAttackSound();
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
                SoundTiming(aiManager);
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
