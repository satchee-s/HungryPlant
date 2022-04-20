using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRoom : MonoBehaviour
{
    RaycastHit hit;
    DoorControl door;
    [SerializeField] float detectionDistance;
    //[SerializeField] bool isPlaying;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                door = hit.collider.GetComponent<DoorControl>();
                if (!door.open && !door.locked)
                {
                    door.Interact();
                    //PlayAnimation();
                    door = null;
                }
            }
        }
    }

    public void PlayAnimation()
    {
        StartCoroutine("EnterRoom");
    }
    IEnumerator EnterRoom()
    {
        //play animation
        Debug.Log("Animation playing");
        yield return null;
    }
}
