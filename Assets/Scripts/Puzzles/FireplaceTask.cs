using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceTask : MonoBehaviour
{
    public bool isBucketFilled;
    public GameObject fire;
    //public GameObject extinguishedFireSmoke;
    //public GameObject deskDrawerKey;
    //public AudioSource extinguishedFireSound;
    
    void Update()
    {
        if (isBucketFilled)
        {
            Destroy(fire);
            //Instantiate(extinguishedFireSmoke);
            //Instantiate(deskDrawerKey);
            //extinguishedFireSound.Play();
        }
    }
}
