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

    public UnityEvent extras;

    void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        triggered = false;
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
}
