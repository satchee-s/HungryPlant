using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLabTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] AudioSource slither;

    [SerializeField] GameObject greenhouseAudioTrigger;
    [SerializeField] GameObject bubbles;
    [SerializeField] GameObject trigger;

    [SerializeField] Collider boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Bubbles());
        boxCollider.enabled = false;
    }

    IEnumerator Bubbles()
    {
        animator.SetBool("Move", true);
        slither.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(bubbles);
        greenhouseAudioTrigger.SetActive(true);
        Destroy(trigger);
    }

}
