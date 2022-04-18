using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySuspenseMusic : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    [SerializeField] AudioSource audio;
    [SerializeField] Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Hallway());
        collider.gameObject.SetActive(false);
    }

    IEnumerator Hallway()
    {
        audio.Play();
        yield return new WaitForSeconds(10f);
        Destroy(trigger);
    }
}
