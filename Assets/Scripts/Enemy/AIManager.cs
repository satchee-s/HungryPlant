using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public State currentState;
    public State roamingBehavior, captureBehavior, chaseBehavior;
    public AudioClip[] attack;
    public AudioClip[] sounds;

    public VignetteToggle vignette;
    public AudioSource plantSource;
    public Animator animator;

    public Transform player;
    public float stunDistance;
    public float stunDuration;
    float stunTime;
    public bool stunned { get; private set; }

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Vector3.Distance(transform.position, player.position) < stunDistance)
            {
                stunned = true;
                animator.SetTrigger("Stunned");
                PlayAttackSound();
                currentState.enabled = false;
            }
        }

        if (stunned)
        {
            stunTime += Time.deltaTime;
            if (stunTime > stunDuration)
            {
                stunTime = 0;
                stunned = false;
                currentState.enabled = true;
            }
        }
        currentState.SetBehaviour(this);

        if (currentState == chaseBehavior)
            vignette.AddIn();
        else
            vignette.TakeOut();
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
