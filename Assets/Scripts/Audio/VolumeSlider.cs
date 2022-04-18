using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    public VolumeData data;
    private Slider slider;
    public string volumeName;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        ResetSliderValue();
    }

    public void ValueChange(float volume)
    {
        data.SetVolumeLevels(volumeName, volume);
        data.SaveVolumeLevels();
    }

    public void ResetSliderValue()
    {
        if (data != null)
        {
            float volume = data.GetVolumeLevels(volumeName);
            ValueChange(volume);
            slider.value = volume;
        }
    }
}
