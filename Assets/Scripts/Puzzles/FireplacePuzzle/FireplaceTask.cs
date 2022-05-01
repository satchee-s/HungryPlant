using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceTask : Puzzle
{
    public AudioSource extinguishedFireSound;
    
    void Update()
    {

    }

    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
        extinguishedFireSound.Play();
    }
}
