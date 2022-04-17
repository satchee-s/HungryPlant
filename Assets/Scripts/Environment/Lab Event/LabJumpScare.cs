using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabJumpScare : MonoBehaviour
{
    [SerializeField] GameObject trigger;
    [SerializeField] GameObject outsideTrigger;
    //[SerializeField] GameObject bubbles;

    [SerializeField] Collider boxCollider;

    [SerializeField] AudioSource growl;
    [SerializeField] AudioSource slam;
    [SerializeField] AudioSource suspense;
    [SerializeField] AudioSource breathing;

    [SerializeField] Animator animator;

    //opencloseDoor opencloseDoor;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LabScare()); 
        boxCollider.enabled = false;
    }

    IEnumerator LabScare()
    {
        //if (opencloseDoor.open)
        //{
        //    StartCoroutine(opencloseDoor.closing());
        //    slam.Play();
        //}

        animator.Play("Closing");
        slam.Play();
        //yield return new WaitForSeconds(0.5f);
        //bubbles.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        growl.Play();
        yield return new WaitForSeconds(1.2f);
        suspense.Play();
        yield return new WaitForSeconds(1.2f);
        breathing.Play();  
        Destroy(trigger);
        outsideTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(slam);
        Destroy(suspense);
    }
}
