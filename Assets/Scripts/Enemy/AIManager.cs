using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State Patrol, Capture, Chase, Search;

    public void SetMovement (State state)
    {
        currentState = state;
    }

    private void Start()
    {
        currentState = Patrol;
    }

    private void Update()
    {
        currentState.SetBehaviour();
    }
}
