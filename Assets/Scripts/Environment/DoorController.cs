using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool doorOpen = false;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (doorOpen)
        {
            anim.Play("doorclose");
            doorOpen = false;
        }
        else if (!doorOpen)
        {
            anim.Play("dooropen");
            doorOpen = true;
        }
    }
}
