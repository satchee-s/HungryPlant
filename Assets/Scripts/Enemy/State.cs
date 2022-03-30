using UnityEngine;
using System.Collections.Generic;

public abstract class State : MonoBehaviour
{
    //protected AIManager aiManager;
    protected Pathfinding pathfinding;
    protected Transform plant;
    protected Transform player;
    protected SubtitleSystem subtitleSystem;
    RaycastHit hit;

    private void Start()
    {
        //aiManager = FindObjectOfType<AIManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        plant = GameObject.FindWithTag("Plant").GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent <Transform>();
    }

    public abstract void SetBehaviour(AIManager aiManager);

    public bool DetectPlayer(Transform target, Transform viewer, float maxDistance = 10f, float maxDegrees = 30f)
    {
        /*RaycastHit hit;
        //if (Physics.Raycast(viewer.position, viewer.forward, out hit, maxDistance))
        if (Physics.SphereCast(viewer.position, 3f, viewer.forward, out hit, maxDistance))
        {
            if (hit.collider.transform == target)
            {
                return true;
            }
        }
        return false;*/
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
}
