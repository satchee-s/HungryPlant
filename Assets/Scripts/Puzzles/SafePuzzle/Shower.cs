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
    public IEnumerator WaterSpray()
    {
        GameObject particle = Instantiate(shower, position) as GameObject;
        yield return new WaitForSeconds(4f);
        Destroy(particle);
    }

    public void ToggleShower()
    {
        StartCoroutine(WaterSpray());
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
