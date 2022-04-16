using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseDoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject bubbles;
    [SerializeField] GameObject trigger;
    [SerializeField] AudioSource bubblesAudio;
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(trigger);
        bubbles.gameObject.SetActive(true);
        animator.SetBool("Move", true);
        bubblesAudio.Play();
    }
}
