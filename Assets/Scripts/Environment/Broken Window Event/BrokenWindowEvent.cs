using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWindowEvent : MonoBehaviour
{
    [SerializeField] GameObject windowTrigger;
    [SerializeField] GameObject windowTrigger2;
    [SerializeField] GameObject vine;
    
    [SerializeField] ParticleSystem glassParticles;

    [SerializeField] AudioSource glassBreak;
    [SerializeField] AudioSource bubblesSound;

    [SerializeField] Collider collider;
    [SerializeField] Collider collider2;

    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WindowBreak());
        collider.enabled = false;
        collider2.enabled = false;
    }

    IEnumerator WindowBreak()
    {
        animator.SetBool("Move", true);
        glassBreak.Play();
        bubblesSound.Play();
        yield return new WaitForSeconds(0.5f);       
        glassParticles.Play();
        yield return new WaitForSeconds(5f);
        Destroy(windowTrigger);
        Destroy(windowTrigger2);
    }
}
