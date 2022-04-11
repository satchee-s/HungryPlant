using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] GameObject lampShade;
    [SerializeField] GameObject light;

    [SerializeField] AudioSource lightBuzz;

    [SerializeField] Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Flicker());
        collider.enabled = false;
    }

    IEnumerator Flicker()
    {
        lampShade.gameObject.SetActive(false);
        light.gameObject.SetActive(false);
        lightBuzz.Play();
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(true);
        light.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        lampShade.gameObject.SetActive(false);
        light.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(true);
        light.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(false);
        light.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(true);
        light.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(false);
        light.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(true);
        light.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(false);
        light.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        lampShade.gameObject.SetActive(true);
        light.gameObject.SetActive(true);
    }
}
