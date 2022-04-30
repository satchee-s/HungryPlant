using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Puzzle
{
    SafePuzzle safe;
    public GameObject shower;
    public Transform position;
    public bool known;

    private void Start()
    {
        known = false;
    }
    public void WaterSpray()
    {
        GameObject particle = Instantiate(shower, position) as GameObject;
    }

    public void ToggleShower()
    {
        WaterSpray();
    }

    public override void ExecutePuzzle()
    {
        if (known)
            base.ExecutePuzzle();
    }

    public void InformationFound()
    {
        known = true;
    }
}
