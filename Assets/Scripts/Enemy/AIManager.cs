using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State roamingBehavior, captureBehavior, chaseBehavior, searchBehavior;
    public Animator anim;
    public AnimationClip enter;
    public void SetMovement (State state)
    {
        currentState = state;
    }

    private void Start()
    {
        currentState = roamingBehavior;
    }

    private void Update()
    {
        currentState.SetBehaviour(this);
    }

    public IEnumerator EnterRoom()
    {
        anim.Play(enter.name);
        Debug.Log("Entering room");
        yield return new WaitForSeconds(0.5f);
    }
}
