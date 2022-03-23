using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State roamingBehavior, captureBehavior, chaseBehavior, searchBehavior;
    //public Transform plant;
    //public Pathfinding pathfinding;

    public void SetMovement (State state)
    {
        currentState = state;
    }

    private void Start()
    {
        currentState = roamingBehavior;
        //SetMovement(currentState);
        //pathfinding.allNodes = GameObject.FindObjectsOfType<Node>();
        //pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        currentState.SetBehaviour();
    }
}
