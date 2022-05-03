using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : State
{
    [SerializeField] Transform resetPosition;
    [SerializeField] Transform plantResetPosition;
    [SerializeField] AudioSource eatSound;
    [SerializeField] Animator anim;

    bool caught;
    public override void SetBehaviour(AIManager aiManager)
    {
        if (!caught)
            StartCoroutine(Capturing(aiManager));
    }

    IEnumerator Capturing(AIManager aiManager)
    {
        caught = true;
        eatSound.Play();
        anim.SetBool("Capture", true);
        //subtitleSystem.DisplaySubtitle("ahhhh! [gets eaten]");

        yield return new WaitForSeconds(.5f);
        yield return new WaitWhile(() => subtitleSystem.isPlaying);

        player.GetComponent<CharacterController>().enabled = false;
        player.position = resetPosition.position;
        player.GetComponent<CharacterController>().enabled = true;

        anim.SetBool("Capture", false);
        transform.position = plantResetPosition.position;
        aiManager.SetMovement(aiManager.roamingBehavior);
        caught = false;
    }
}
