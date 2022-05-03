using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{

    public Image blackOut;
    Color blackOutColor;

    public string[] captureTexts;
    SubtitleSystem subtitleSystem;

    bool fadeToBlack;
    public float subtitleDelay;
    bool queuedSubtitle;
    public float fadeSpeed;
    public float fadeDuration;

    public LookAtConstraint lookAtConstraint;
    public Transform target;
    ConstraintSource constraintSource;
    [Range(0, 1)] public float weight;
    public float lookSpeed;
    bool looking;

    public bool invokeExtras;
    public UnityEvent extras;

    public bool playFinalAudio;
    public AudioSource lighter;
    public AudioSource catchingFire;
    public float audioDelay;
    public float gapBetween;

    private void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        constraintSource = new ConstraintSource();
        constraintSource.weight = 1;
        constraintSource.sourceTransform = target;
    }

    public void FadeScreen()
    {
        if (!fadeToBlack)
        {
            fadeToBlack = true;
            StartCoroutine(FadeToBlack());
        }        
    }

    public void EnableLookAt()
    {
        if (!looking)
        {
            StartCoroutine(LookAtPlant(true));
            looking = true;
        }
    }

    public void DisableLookAt()
    {
        if (looking)
        {
            StartCoroutine(LookAtPlant(false));
            looking = false;
        }
    }

    IEnumerator FadeToBlack()
    {
        blackOutColor = blackOut.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOut.color.a < 1)
            {
                fadeAmount = blackOutColor.a + (fadeSpeed * Time.deltaTime);
                blackOutColor = new Color(blackOutColor.r, blackOutColor.g, blackOutColor.b, fadeAmount);
                blackOut.color = blackOutColor;
                yield return null;
            }
            if (!queuedSubtitle && captureTexts.Length > 0)
            {
                subtitleSystem.DisplaySubtitle(captureTexts[Random.Range(0, captureTexts.Length)], 2, .08f, subtitleDelay);
                queuedSubtitle = true;
            }            
            
            if (captureTexts.Length > 0)
            {
                yield return new WaitUntil(() => subtitleSystem.isPlaying);
                yield return new WaitWhile(() => subtitleSystem.isPlaying);
            } 
            
            if (playFinalAudio)
            {
                yield return new WaitForSeconds(audioDelay);
                lighter.Play();
                yield return new WaitForSeconds(gapBetween);
                catchingFire.Play();
            }

            yield return new WaitForSeconds(fadeDuration);

            if (invokeExtras)
                extras.Invoke();

            while (blackOut.color.a > 0)
            {
                fadeAmount = blackOutColor.a - ((fadeSpeed / 2) * Time.deltaTime);
                blackOutColor = new Color(blackOutColor.r, blackOutColor.g, blackOutColor.b, fadeAmount);
                blackOut.color = blackOutColor;
                yield return null;
            }
            fadeToBlack = false;
            queuedSubtitle = false;
        }
    }

    IEnumerator LookAtPlant(bool look)
    {
        if (look)
        {
            lookAtConstraint.constraintActive = true;
            lookAtConstraint.SetSource(0, constraintSource);
            while (weight < 1)
            {
                weight += Time.deltaTime * lookSpeed;
                lookAtConstraint.weight = weight;
                yield return null;
            }
            lookAtConstraint.GetComponent<MouseMovement>().enabled = false;
        }
        else
        {
            while (weight > 0)
            {
                weight -= Time.deltaTime * lookSpeed;
                lookAtConstraint.weight = weight;
                yield return null;
            }
            lookAtConstraint.constraintActive = false;
            lookAtConstraint.GetComponent<MouseMovement>().enabled = true;
        }
    }
}
