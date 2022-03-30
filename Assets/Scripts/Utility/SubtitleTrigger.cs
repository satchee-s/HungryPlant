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
    bool triggered;

    public UnityEvent extras;

    void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && colliderTrigger)
        {
            TriggerSubtitle();
        }
    }

    public void TriggerSubtitle()
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
}
