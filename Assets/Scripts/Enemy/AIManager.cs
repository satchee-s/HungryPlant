using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State roamingBehavior, captureBehavior, chaseBehavior;
    public AudioClip[] attack;
    public AudioClip[] sounds;

    public AudioSource plantSource;

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

    public void PlayAttackSound()
    {
        if (!plantSource.isPlaying)
        {
            int index = Random.Range(0, attack.Length);
            plantSource.PlayOneShot(attack[index]);
        }
    }

    public void PlaySound()
    {
        if (!plantSource.isPlaying)
        {
            int index = Random.Range(0, sounds.Length);
            plantSource.PlayOneShot(sounds[index]);
        }
    }
}
