using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameplaySettings")]
public class GameplaySettings : ScriptableObject
{
    public string prefPrefix = "Gameplay_";
    public float defaultSensitivity;
    public float currentSensitivity;

    public float GetSensitivity()
    {
        if (PlayerPrefs.HasKey(prefPrefix + "Sensitivity"))
        {
            currentSensitivity = PlayerPrefs.GetFloat(prefPrefix + "Sensitivity");
            return currentSensitivity;
        }
        else
        {
            return defaultSensitivity;
        }
    }

    public void SetSensitivity(float value)
    {
        currentSensitivity = value;
    }

    public void SaveSensitivity()
    {
        PlayerPrefs.SetFloat(prefPrefix + "Sensitivity", currentSensitivity);
    }
}
