using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        BlackoutTrigger blackoutTrigger = gameObject.GetComponent<BlackoutTrigger>();
        blackoutTrigger.Flicker();
    }
}
