using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSystem : MonoBehaviour
{

    private Text subtitle;

    private void Start()
    {
        subtitle = GetComponent<Text>();
        subtitle.text = "";
    }

    public void DisplaySubtitle(string text, float endTime = 2, float interval = .04f, float delay = 0)
    {
        StartCoroutine(Subtitle(subtitle, text, endTime, interval, delay));
    }

    public void CompletedText(string text)
    {
        StartCoroutine(Subtitle(subtitle, text, 2, .04f, 0));
    }
    
    IEnumerator Subtitle(Text holder, string subtitle, float endTime, float interval, float delay)
    {
        yield return new WaitForSeconds(delay);

        holder.text = "";
        for (int i = 1; i <= subtitle.Length; i++)
        {

            holder.text = subtitle.Substring(0, i);
            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(endTime);

        holder.text = "";
    }
}