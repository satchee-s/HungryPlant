using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject introUI;

    [SerializeField] AudioSource introNoise;

    public PlayerMovement playerMovement;

    private void Start()
    {
        StartCoroutine(Introo());
    }

    IEnumerator Introo()
    {
        float tempSpeed = playerMovement.speed;
        playerMovement.speed = 0;
        yield return new WaitForSeconds(3f);
        introNoise.Play();
        yield return new WaitForSeconds(10f);
        playerMovement.speed = tempSpeed;
        inventoryUI.gameObject.SetActive(true);
        Destroy(introUI);            
    }
}
