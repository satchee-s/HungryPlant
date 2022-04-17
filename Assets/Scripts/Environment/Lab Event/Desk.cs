using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    [SerializeField] GameObject doorTrigger;
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject doorLight;
    [SerializeField] GameObject bubbles;

    private void OnTriggerEnter(Collider other)
    {
        doorTrigger.gameObject.SetActive(true);
        Destroy(trigger);
        Destroy(doorLight);
        bubbles.gameObject.SetActive(true);
    }
}
