using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideTrigger : MonoBehaviour
{
    [SerializeField] Text kablamo;
    [SerializeField] Text getOut;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            kablamo.gameObject.SetActive(true);
            Destroy(getOut);
        }
    }
}
