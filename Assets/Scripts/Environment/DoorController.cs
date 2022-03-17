using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool doorOpen = false;
    [HideInInspector] public bool canBeOpened;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (canBeOpened)
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
}
