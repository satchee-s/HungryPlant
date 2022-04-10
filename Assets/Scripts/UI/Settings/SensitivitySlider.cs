using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{

    public GameplaySettings data;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        ResetSliderValue();
    }

    public void ValueChange(float value)
    {
        data.SetSensitivity(value);
        data.SaveSensitivity();
    }

    public void ResetSliderValue()
    {
        if (data != null)
        {
            float sensitivity = data.GetSensitivity();

            ValueChange(sensitivity);
            slider.value = sensitivity;
        }
    }
}
