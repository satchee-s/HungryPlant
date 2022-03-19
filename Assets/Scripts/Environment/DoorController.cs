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
            anim.Play("DoorClose");
            doorOpen = false;
            Debug.Log("Door close animation");
        }
        else if (!doorOpen)
        {
            anim.Play("DoorOpen");
            doorOpen = true;
            Debug.Log("Door open animation");

        }
    }
}
