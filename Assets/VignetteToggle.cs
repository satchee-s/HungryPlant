using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VignetteToggle : MonoBehaviour
{

    Volume vignette;
    bool toggled;
    public float speed;
    float weight;

    // Start is called before the first frame update
    void Start()
    {
        vignette = GetComponent<Volume>();
    }

    public void AddIn()
    {
        if (!toggled)
            ChangeWeight(true);
    }

    public void TakeOut()
    {
        if (toggled)
            ChangeWeight(false);
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
