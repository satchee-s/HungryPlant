using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySuspenseMusic : MonoBehaviour
{
    //[SerializeField] GameObject trigger;
    [SerializeField] AudioSource audio;

    public void PlayAudio()
    {
        audio.Play();
    }
}
