using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool doorOpen = false;
    [HideInInspector] public bool isNotBarricaded;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isNotBarricaded = true;        
    }

    public void PlayAnimation()
    {
        if (isNotBarricaded)
        {
            if (doorOpen)
            {
                //anim.Play("doorclose");
                Debug.Log("Closing");
                doorOpen = false;
                anim.SetBool("Open", doorOpen);
            }
            else if (!doorOpen)
            {
                //anim.Play("dooropen");
                Debug.Log("Opening");
                doorOpen = true;
                anim.SetBool("Open", doorOpen);
            }
        }        
    }

    public void Lock(bool locked)
    {
        anim.SetBool("Locked", locked);
    }
}
