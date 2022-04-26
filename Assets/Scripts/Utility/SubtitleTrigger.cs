using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubtitleTrigger : MonoBehaviour
{
    SubtitleSystem subtitleSystem;
    public string subtitle;
    public bool triggerOnce;
    public bool colliderTrigger;
    public bool mouseoverTrigger;
    public bool isDelayed;
    public float delayTimer;
    bool triggered;

    public bool slowPlayer;
    public PlayerMovement player;
    float initialSpeed, sprintSpeed;

    public UnityEvent extras;

    void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        triggered = false;

        if (player != null)
        {
            initialSpeed = player.speed;
            sprintSpeed = player.sprintSpeed;
        }            
    }

    private void OnMouseEnter()
    {
        if (mouseoverTrigger)
        {
            if (!isDelayed)
            {
                Subtitle();
            }
            else
            {
                StartCoroutine(SubtitleTimer());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && colliderTrigger)
        {
            if (!isDelayed)
            {
                Subtitle();
            }
            else
            {
                StartCoroutine(SubtitleTimer());
            }
            
        }
    }

    public void TriggerSubtitle()
    {
        if (!isDelayed)
        {
            Subtitle();
        }
        else
        {
            StartCoroutine(SubtitleTimer());
        }
    }

    void Subtitle()
    {
        if (!triggered)
        { 
            subtitleSystem.DisplaySubtitle(subtitle);
            extras.Invoke();
            if (slowPlayer && player != null)
                StartCoroutine(SlowPlayer());
        }

        if (triggerOnce)
        {
            triggered = true;
        }
    }

    IEnumerator SubtitleTimer()
    {
        yield return new WaitForSeconds(delayTimer);
        Subtitle();
    }

    IEnumerator SlowPlayer()
    {
        player.speed = initialSpeed * .4f;
        sprintSpeed = initialSpeed * .4f;

        yield return new WaitForSeconds(.25f);

        while (true)
        {
            if (!subtitleSystem.isPlaying)
            {
                player.speed = initialSpeed;
                player.sprintSpeed = sprintSpeed;
                break;
            }
            yield return null;
        }        
    }
}
