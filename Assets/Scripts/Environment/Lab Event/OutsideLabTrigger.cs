using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLabTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] AudioSource slither;
    [SerializeField] AudioSource hallwaySuspense;

    [SerializeField] GameObject greenhouseAudioTrigger;
    [SerializeField] GameObject bubbles;
    [SerializeField] GameObject trigger;

    [SerializeField] Collider boxCollider;

    public GameObject dialog;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Bubbles());
        boxCollider.enabled = false;
        dialog.SetActive(true);
    }

    IEnumerator Bubbles()
    {
        animator.SetBool("Move", true);
        slither.Play();
        yield return new WaitForSeconds(1.5f);
        hallwaySuspense.Play();
        Destroy(bubbles);
        greenhouseAudioTrigger.SetActive(true);
        Destroy(trigger);
    }

}
