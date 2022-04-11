using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseAudioTrigger : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject trigger;

    [SerializeField] Collider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Sound());
        boxCollider.enabled = false;
    }

    IEnumerator Sound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        Destroy(audioSource);
        Destroy(trigger);
    }
}
