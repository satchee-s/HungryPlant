using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : State
{
    [SerializeField] Transform resetPosition;
    [SerializeField] Transform plantResetPosition;
    public override void SetBehaviour(AIManager aiManager)
    {
        //subtitleSystem.DisplaySubtitle("ahhhh! [gets eaten]");
        player.GetComponent<CharacterController>().enabled = false;
        player.position = resetPosition.position;
        player.GetComponent<CharacterController>().enabled = true;
        pathfinding.ClearPath();
        transform.position = plantResetPosition.position;
        aiManager.SetMovement(aiManager.roamingBehavior);
    }
}
