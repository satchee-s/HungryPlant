using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceTask : Puzzle
{
    public Tap tap;
    public GameObject fire;
    //public GameObject extinguishedFireSmoke;
    public GameObject deskDrawerKey;
    //public AudioSource extinguishedFireSound;
    
    void Update()
    {
        //if (tap.isBucketFilled)
        //{
        //    Destroy(fire);
        //    //Instantiate(extinguishedFireSmoke);
        //    //Instantiate(deskDrawerKey);
        //    //extinguishedFireSound.Play();
        //}
    }

    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
    }
}
