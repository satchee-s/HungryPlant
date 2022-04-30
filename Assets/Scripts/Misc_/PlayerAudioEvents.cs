using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField] AudioSource step1;
    [SerializeField] AudioSource step2;
    [SerializeField] AudioSource flash;
    [SerializeField] AudioSource glassBreak;

    void StepSound1() { step1.Play(); }
   
    void StepSound2() { step2.Play(); }

    void FlashSound() { flash.Play(); }

    //void GlassSound() { glassBreak.Play(); }

}
