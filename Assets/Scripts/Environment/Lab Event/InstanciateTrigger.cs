using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    [SerializeField] GameObject doorTrigger;
    [SerializeField] GameObject trigger;

    private void OnTriggerEnter(Collider other)
    {
        doorTrigger.gameObject.SetActive(true);
        Destroy(trigger);
    }
}
