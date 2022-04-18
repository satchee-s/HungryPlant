using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSystem : MonoBehaviour
{
    public float periodPause;
    public float commaPause;
    public float questionMarkPause;
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
        char[] chars = subtitle.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {            
            holder.text += chars[i];

            if (chars[i] == '.')
            {
                Debug.Log("Pausing due to '.'");
                yield return new WaitForSeconds(periodPause);
            }
            if (chars[i] == ',')
            {
                Debug.Log("Pausing due to ','");
                yield return new WaitForSeconds(commaPause);
            }
            if (chars[i] == '?')
            {
                Debug.Log("Pausing due to ','");
                yield return new WaitForSeconds(questionMarkPause);
            }

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(endTime);

        holder.text = "";
    }
}