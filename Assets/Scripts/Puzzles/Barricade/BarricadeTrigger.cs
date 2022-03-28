using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeTrigger : MonoBehaviour
{
    [SerializeField]Barricade barricade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            barricade.SetTrigger(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            barricade.SetTrigger(false);
        }
    }
}
