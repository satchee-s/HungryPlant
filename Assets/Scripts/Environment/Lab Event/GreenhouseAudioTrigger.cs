using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseAudioTrigger : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject insideGreenhouseTrigger;

    [SerializeField] Collider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Sound());
        boxCollider.enabled = false;
        insideGreenhouseTrigger.gameObject.SetActive(true);
    }

    IEnumerator Sound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        Destroy(audioSource);
        Destroy(trigger);
    }
}
