using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentalComment : MonoBehaviour
{

    public string comment;
    SubtitleSystem subtitleSystem;
    bool triggered;

    public UnityEvent extras;

    private void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
    }

    public void TriggerComment()
    {
        if (!triggered)
        {
            subtitleSystem.DisplaySubtitle(comment);
            triggered = true;
            if(GetComponent<Collider>() != null)
                GetComponent<Collider>().enabled = false;

            extras.Invoke();
        }
    }
}
