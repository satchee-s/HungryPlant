using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHallLights : MonoBehaviour
{

    Light light;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        isOn = false;
    }

    private void OnBecameVisible()
    {
        if (!isOn)
            light.enabled = true;
    }

    private void OnBecameInvisible()
    {
        if (isOn)
            light.enabled = false;
    }
}
