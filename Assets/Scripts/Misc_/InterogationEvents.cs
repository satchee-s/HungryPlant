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
    public string menuScene;
    public float alphaChange;
    public Text credits;
    public Image[] buttons;

    private void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();

        credits.color = new Color(1, 1, 1, 0);
        for (int i = 0; i < buttons.Length; i++)
        {
            Color temp = buttons[i].color;
            buttons[i].color = new Color(temp.r, temp.g, temp.b, 0);
        }
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

    public void ShowCredits()
    {
        StartCoroutine(DisplayUI());
    }

    IEnumerator DisplayUI()
    {
        yield return new WaitUntil(() => subtitleSystem.isPlaying);
        yield return new WaitWhile(() => subtitleSystem.isPlaying);

        float creditA;
        float buttonA;
        while (credits.color.a < 1)
        {
            creditA = credits.color.a + (alphaChange * Time.deltaTime);
            credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, creditA);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (buttons[0].color.a < 1)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonA = buttons[i].color.a + (alphaChange * Time.deltaTime);
                buttons[i].color = new Color(buttons[i].color.r, buttons[i].color.g, buttons[i].color.b, buttonA);
            }
            yield return null;
        }
    }

    public void ShowCursor(bool show)
    {
        if (show)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void ToMenu()
    {
        changeScene = true;
        if (!fadeToBlack)
        {
            fadeToBlack = true;
            StartCoroutine(FadeToBlack(menuScene));
        }
    }

    public void Quit()
    {
        changeScene = true;
        if (!fadeToBlack)
        {
            fadeToBlack = true;
            StartCoroutine(FadeToBlack(true));
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

    IEnumerator FadeToBlack(bool quit)
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

            subtitleSystem.DisplaySubtitle("Thank you for Playing", 3, .1f, .5f);
            yield return new WaitUntil(() => subtitleSystem.isPlaying);
            yield return new WaitWhile(() => subtitleSystem.isPlaying);

            if (quit)
            {
                Application.Quit();
            }
        }
    }

    IEnumerator FadeToBlack(string scene)
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

            subtitleSystem.DisplaySubtitle("Thank you for Playing", 3, .1f, .5f);
            yield return new WaitUntil(() => subtitleSystem.isPlaying);
            yield return new WaitWhile(() => subtitleSystem.isPlaying);

            if (changeScene)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
