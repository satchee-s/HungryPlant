using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryIncrease : MonoBehaviour
{

    public float batteryIncrease = 50; 
    FlashlightController flashlightController;

    // Start is called before the first frame update
    void Start()
    {
        flashlightController = FindObjectOfType<FlashlightController>();
    }

    public void BatteryInteraction()
    {
        flashlightController.AddCharge(batteryIncrease);
    }
}
