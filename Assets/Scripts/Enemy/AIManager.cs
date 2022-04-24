using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State roamingBehavior, captureBehavior, chaseBehavior;
    public AudioClip[] attack;
    public AudioClip[] shortSounds;
    public AudioClip[] longSounds;

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

    public void PlayShortSound()
    {
        if (!plantSource.isPlaying)
        {
            int index = Random.Range(0, shortSounds.Length);
            plantSource.PlayOneShot(shortSounds[index]);
        }
    }

    public void PlayLongSound()
    {
        if (!plantSource.isPlaying)
        {
            int index = Random.Range(0, longSounds.Length);
            plantSource.PlayOneShot(longSounds[index]);
        }
    }
}
