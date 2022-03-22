using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject thingToTrigger;


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ScriptedAnimation());
    }

    IEnumerator ScriptedAnimation()
    {
        Instantiate(thingToTrigger);
        animator.SetBool("move", true);
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(thingToTrigger);
    }


}
