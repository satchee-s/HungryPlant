using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject introUI;

    [SerializeField] AudioSource introNoise;
    [SerializeField] AudioSource getUp;

    public PlayerMovement playerMovement;
    public MouseMovement mouseMovement;

    private void Start()
    {
        StartCoroutine(Introo());
    }

    IEnumerator Introo()
    {
        float tempSpeed = playerMovement.speed;
        float tempMouseSensitivity = mouseMovement.mouseSensitivity;
        mouseMovement.mouseSensitivity = 0;
        playerMovement.speed = 0;
        yield return new WaitForSeconds(3f);
        introNoise.Play();
        yield return new WaitForSeconds(2f);
        getUp.Play();
        yield return new WaitForSeconds(8f);
        playerMovement.speed = tempSpeed;
        mouseMovement.mouseSensitivity = tempMouseSensitivity;
        inventoryUI.gameObject.SetActive(true);
        Destroy(introUI);            
    }
}
