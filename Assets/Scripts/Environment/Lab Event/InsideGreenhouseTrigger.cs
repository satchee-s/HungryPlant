using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideGreenhouseTrigger : MonoBehaviour
{
    [SerializeField] GameObject doorTrigger;
    [SerializeField] GameObject trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doorTrigger.gameObject.SetActive(true);
            Destroy(trigger);
        }            
    }
}
