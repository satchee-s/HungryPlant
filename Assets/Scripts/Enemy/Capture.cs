using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : State
{
    [SerializeField] Transform resetPosition;
    public override void SetBehaviour(AIManager aiManager)
    {
        aiManager.SetMovement(aiManager.captureBehavior);
        subtitleSystem.DisplaySubtitle("ahhhh! [gets eaten]");
        player.position = resetPosition.position;
        aiManager.SetMovement(aiManager.roamingBehavior);
    }
}
