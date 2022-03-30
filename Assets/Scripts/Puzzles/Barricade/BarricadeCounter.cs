using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeCounter : Puzzle
{
    public Barricade[] barricades;
    public string completedText;
    SubtitleSystem subtitleSystem;

    private void Start()
    {
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        SetBarricadeStates(false);
    }

    public void CheckBarricades()
    {
        int completed = 0;
        for (int i = 0; i < barricades.Length; i++)
        {
            if (barricades[i].completed)
            {
                completed++;
            }
        }

        if (completed >= barricades.Length * .8f)
        {
            taskCompleted.Invoke();
            subtitleSystem.DisplaySubtitle(completedText);
        }
    }

    public void SetBarricadeStates(bool state)
    {
        for (int i = 0; i < barricades.Length; i++)
        {
            barricades[i].enabled = state;
        }
    }
}
