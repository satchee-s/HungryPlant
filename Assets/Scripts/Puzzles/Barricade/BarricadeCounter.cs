using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeCounter : Puzzle
{
    public Barricade[] barricades;
    public string completedText;

    bool firstBarricade;
    public string firstBarricadeText;

    private void Start()
    {
        firstBarricade = false;
        subtitle = FindObjectOfType<SubtitleSystem>();
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

        Debug.Log(completed + " Barricades done");

        if (completed == 0 && !firstBarricade)
        {
            subtitle.DisplaySubtitle(firstBarricadeText);
            firstBarricade = true;
        }

        if (completed >= barricades.Length * .8f)
        {
            taskCompleted.Invoke();
            subtitle.DisplaySubtitle(completedText);
        }
    }

    public void SetBarricadeStates(bool state)
    {
        for (int i = 0; i < barricades.Length; i++)
        {
            barricades[i].startPuzzle = state;
        }
    }
}
