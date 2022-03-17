using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    bool hidingState = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!hidingState)
                { 
                    other.GetComponent<PlayerMovement>().enabled = false;
                    other.GetComponentInChildren<MouseMovement>().isHiding = true;
                    hidingState = true;
                    //play animation/ add shader
                }
                else if (hidingState)
                {
                    other.GetComponent<PlayerMovement>().enabled = true;
                    other.GetComponentInChildren<MouseMovement>().isHiding = false;
                    hidingState = false;
                    //play animation/ add shader
                }

            }
        }
    }
}
