using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VignetteToggle : MonoBehaviour
{

    public Volume vignette;
    bool toggled;
    public float speed;
    float weight;

    public bool IsToggled()
    {
        return toggled;
    }

    public void AddIn()
    {
        if (!toggled)
            StartCoroutine(ChangeWeight(true));            
    }

    public void TakeOut()
    {
        if (toggled)
            StartCoroutine(ChangeWeight(false));
    }

    IEnumerator ChangeWeight(bool state)
    {
        if (state)
        {
            while (weight < 1)
            {
                weight += Time.deltaTime * speed;
                vignette.weight = weight;
                yield return null;
            }
            toggled = true;
        }
        else
        {
            while (weight > 0)
            {
                weight -= Time.deltaTime * speed;
                vignette.weight = weight;
                yield return null;
            }
            toggled = false;
        }
    }
}
