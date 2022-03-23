using System.Collections;
using UnityEngine;

public class DrawerOpen : MonoBehaviour
{
    [SerializeField] Animator drawer;
    [SerializeField] Transform player;
    //[SerializeField] AudioSource drawerSound;

    bool isopen;

    void Start()
    {
        isopen = false;
    }

    private void OnMouseOver()
    {
        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < 10 && (isopen == false) && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Open());
            }

            if (distance < 10 && (isopen == true) && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Close());
            }
        }
    }

    IEnumerator Open()
    {
        drawer.Play("Open");
        //drawerSound.Play();
        isopen = true;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Close()
    {
        drawer.Play("Close");
        //drawerSound.Play();
        isopen = false;
        yield return new WaitForSeconds(0.5f);
    }
}
