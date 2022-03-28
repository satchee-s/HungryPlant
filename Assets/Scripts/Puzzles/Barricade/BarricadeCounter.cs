using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeCounter : Puzzle
{

    public Barricade[] barricades;

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
        }
    }
}
