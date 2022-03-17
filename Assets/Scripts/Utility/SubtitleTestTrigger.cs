using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleTestTrigger : MonoBehaviour
{

    private SubtitleSystem subtitleSystem;
    public string subtitle;
    public bool triggerOnce;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !triggered)
        {
            subtitleSystem.DisplaySubtitle(subtitle);
            
            if (triggerOnce)
            {
                triggered = true;
            }
        }
    }
}
