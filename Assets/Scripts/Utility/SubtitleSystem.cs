using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSystem : MonoBehaviour
{
    public float periodPause;
    public float commaPause;
    public float questionMarkPause;
    private Text subtitleHolder;

    float endTime = 2;
    float interval = .05f;
    float delay = 0;

    public bool isPlaying { get; private set; }
    Queue<string> subtitlesQueue = new Queue<string>();

    private void Start()
    {
        subtitleHolder = GetComponent<Text>();
        subtitleHolder.text = "";

        StartCoroutine(Subtitle());
    }

    public void DisplaySubtitle(string text, float endTime = 2, float interval = .04f, float delay = 0)
    {
        this.endTime = endTime;
        this.interval = interval;
        this.delay = delay;
        subtitlesQueue.Enqueue(text);
    }

    public void CompletedText(string text)
    {
        subtitlesQueue.Enqueue(text);
    }
    
    IEnumerator Subtitle()
    {
        while (true)
        {            
            if (subtitlesQueue.Count > 0)
            {
                yield return new WaitForSeconds(delay);

                isPlaying = true;
                string subtitle = subtitlesQueue.Dequeue();
                subtitleHolder.text = "";
                char[] chars = subtitle.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    subtitleHolder.text += chars[i];

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

                subtitleHolder.text = "";
            }
            else
            {
                isPlaying = false;
            }
            yield return null;
        }        
    }
}