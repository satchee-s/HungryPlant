using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : State
{
    [SerializeField] Transform resetPosition;
    [SerializeField] Transform plantResetPosition;
    //Patrol patrol;
    public override void SetBehaviour(AIManager aiManager)
    {
        //aiManager.SetMovement(aiManager.captureBehavior);
        subtitleSystem.DisplaySubtitle("ahhhh! [gets eaten]");
        player.GetComponent<CharacterController>().enabled = false;
        player.position = resetPosition.position;
        player.GetComponent<CharacterController>().enabled = true;
        pathfinding.ClearPath();
        //patrol = (Patrol)aiManager.roamingBehavior;
        //patrol.startingNode = plantResetPosition.GetComponent<Node>();
        //patrol.hasPath = false;
        transform.position = plantResetPosition.position;
        aiManager.SetMovement(aiManager.roamingBehavior);
    }
}
