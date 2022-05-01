using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterogationEvents : MonoBehaviour
{
    public Image blackOut;
    Color blackOutColor;
    SubtitleSystem subtitleSystem;

    bool fadeToBlack;
    public float subtitleDelay;
    public float fadeSpeed;
    public float fadeDuration;
    public float timeToFadeBack;

    bool changeScene;
    public string nextScene;

    private void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
    }

    public void FadeScreen()
    {
        if (!fadeToBlack)
        {
            fadeToBlack = true;
            StartCoroutine(FadeToBlack());
        }
    }

    public void ToNextScene()
    {
        changeScene = true;
        if (!fadeToBlack)
        {
            fadeToBlack = true;
            StartCoroutine(FadeToBlack());
        }

    }

    IEnumerator FadeToBlack()
    {
        blackOutColor = blackOut.color;
        float fadeAmount;        

        if (fadeToBlack)
        {
            if (changeScene)
            {
                yield return new WaitForSeconds(timeToFadeBack);
                yield return new WaitUntil(() => !subtitleSystem.isPlaying);
            }
                

            while (blackOut.color.a < 1)
            {
                fadeAmount = blackOutColor.a + (fadeSpeed * Time.deltaTime);
                blackOutColor = new Color(blackOutColor.r, blackOutColor.g, blackOutColor.b, fadeAmount);
                blackOut.color = blackOutColor;
                yield return null;
            }

            yield return new WaitForSeconds(fadeDuration);

            if (changeScene)
            {
                SceneManager.LoadScene(nextScene);
            }

            while (blackOut.color.a > 0)
            {
                fadeAmount = blackOutColor.a - ((fadeSpeed / 2) * Time.deltaTime);
                blackOutColor = new Color(blackOutColor.r, blackOutColor.g, blackOutColor.b, fadeAmount);
                blackOut.color = blackOutColor;
                yield return null;
            }
            fadeToBlack = false;
        }
    }
}
