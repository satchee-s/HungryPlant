using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutTrigger : MonoBehaviour
{
    [SerializeField] GameObject directionalLight;
    [SerializeField] GameObject lights;
    [SerializeField] GameObject trigger;
    [SerializeField] Collider boxCollider;

    [SerializeField] AudioSource powerDown;
    [SerializeField] AudioSource powerOn;
    //[SerializeField] Skybox skybox;


    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Flicker());
        boxCollider.enabled = false;
    }

    public IEnumerator Flicker()
    {
        directionalLight.gameObject.SetActive(false);
        lights.gameObject.SetActive(false);
        powerDown.Play();
        yield return new WaitForSeconds(10f);
        powerOn.Play();
        directionalLight.gameObject.SetActive(true);
        lights.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        directionalLight.gameObject.SetActive(false);
        lights.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        directionalLight.gameObject.SetActive(true);
        lights.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        directionalLight.gameObject.SetActive(false);
        lights.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        directionalLight.gameObject.SetActive(true);
        lights.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(trigger);
    }
}
