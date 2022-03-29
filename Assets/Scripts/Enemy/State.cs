using UnityEngine;
using System.Collections.Generic;

public abstract class State : MonoBehaviour
{
    //protected AIManager aiManager;
    protected Pathfinding pathfinding;
    protected Transform plant;
    protected Transform player;
    protected SubtitleSystem subtitleSystem;

    private void Start()
    {
        //aiManager = FindObjectOfType<AIManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        plant = GameObject.Find("Plant").GetComponent<Transform>();
        player = GameObject.Find("PlayerParent").GetComponent <Transform>();
    }

    public abstract void SetBehaviour(AIManager aiManager);

    public bool DetectPlayer(Transform target, Transform viewer, float maxDistance)
    {
        RaycastHit hit;
        //if (Physics.Raycast(viewer.position, viewer.forward, out hit, maxDistance))
        if (Physics.SphereCast(viewer.position, 3f, viewer.forward, out hit, maxDistance))
        {
            if (hit.collider.transform == target)
            {
                return true;
            }
        }
        return false;
    }
}
