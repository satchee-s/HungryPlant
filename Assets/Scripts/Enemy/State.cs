using UnityEngine;
using System.Collections.Generic;

public abstract class State : MonoBehaviour
{
    protected Pathfinding pathfinding;
    protected Transform player;
    protected SubtitleSystem subtitleSystem;
    RaycastHit hit;
    public float playerDetectionDistance = 10f;

    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        player = GameObject.FindWithTag("Player").GetComponent <Transform>();
    }

    public abstract void SetBehaviour(AIManager aiManager);

    public bool DetectPlayer(Transform target, Transform viewer, float maxDistance, float maxDegrees = 45f)
    {
        float degree = Vector3.Angle(target.position - viewer.position, viewer.forward);
        Vector3 targetDirection = (target.position - viewer.position);
        if (degree < maxDegrees)
        {
            if (Physics.Raycast(viewer.position, targetDirection, out hit, maxDistance))
            {
                if (hit.collider.transform == target)
                    return true;
            }
        }
        else if (Vector3.Distance(viewer.position, target.position) < 4f && Physics.Raycast(viewer.position, viewer.forward, out hit, maxDistance))
        {
            if (hit.collider.transform == target)
                return true;
        }
        return false;
    }

    public bool PlantInRange(Vector3 currentNode)
    {
        float range = 0.2f;
        if (Mathf.Abs(transform.position.x - currentNode.x) <= range && Mathf.Abs(transform.position.z - currentNode.z) <= range)
            return true;
        return false;
    }
}
