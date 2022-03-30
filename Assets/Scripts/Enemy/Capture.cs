using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : State
{
    [SerializeField] Transform resetPosition;
    [SerializeField] Transform plantResetPosition;
    Patrol patrol;
    public override void SetBehaviour(AIManager aiManager)
    {
        //aiManager.SetMovement(aiManager.captureBehavior);
        subtitleSystem.DisplaySubtitle("ahhhh! [gets eaten]");
        player.position = resetPosition.position;
        pathfinding.ClearPath();
        patrol = (Patrol)aiManager.roamingBehavior;
        patrol.startingNode = plantResetPosition.GetComponent<Node>();
        patrol.hasPath = false;
        plant.position = plantResetPosition.position;
        aiManager.SetMovement(aiManager.roamingBehavior);
    }
}
