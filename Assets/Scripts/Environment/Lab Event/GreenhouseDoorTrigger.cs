using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GreenhouseDoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject bubbles;
    [SerializeField] GameObject trigger;
    [SerializeField] AudioSource bubblesAudio;
    [SerializeField] Animator animator;

    public SubtitleTrigger dialog;
    public SubtitleTrigger flashDialog;
    public UnityEvent extras;

    bool waitForFlash;

    public KeyPrompts prompt;
    bool prompted;

    private void Start()
    {
        waitForFlash = false;
        prompted = false;
        prompt = FindObjectOfType<KeyPrompts>();
    }

    private void Update()
    {
        if (waitForFlash)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Resume Time to Normal");
                StartCoroutine(LerpTime(1, .5f, false));
                animator.SetTrigger("Stunned");
                flashDialog.TriggerSubtitle();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Destroy(trigger);
            
            GetComponent<Collider>().enabled = false;
            bubbles.gameObject.SetActive(true);
            animator.SetTrigger("Start");
            bubblesAudio.Play();
            dialog.TriggerSubtitle();
            extras.Invoke();
        }        
    }

    public void SlowTime()
    {
        waitForFlash = true;
        
        StartCoroutine(LerpTime(0.002f, .5f, true));
    }

    IEnumerator LerpTime(float lerpTimeTo, float timeToTake, bool stopTime)
    {
        if (!prompted)
        {
            prompt.DisplayPrompt("R");
            prompted = true;
        }
            
        float end = Time.time + timeToTake;
        float startScale = Time.timeScale;
        if (startScale == 0)
            startScale = .001f;
        float i = 0;
        while (Time.time < end)
        {
            i += (1 / timeToTake) * Time.deltaTime;
            Time.timeScale = Mathf.Lerp(startScale, lerpTimeTo, i);
            Debug.Log("Time Scale is : " + Time.timeScale);
            yield return null;
        }

        Debug.Log("Time changed");
        if (!stopTime)
            Time.timeScale = lerpTimeTo;
        else
            Time.timeScale = 0;
    }
}
