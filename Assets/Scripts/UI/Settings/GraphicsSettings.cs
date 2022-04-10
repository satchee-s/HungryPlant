using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public string prefPrefix = "Graphics_";

    public int defaultQuality;
    public int quality;

    public bool fullscreen;

    Resolution[] resolutions;
    public Dropdown resUI;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resUI.ClearOptions();
        List<string> res = new List<string>();
        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string r = resolutions[i].width + "x" + resolutions[i].height;
            res.Add(r);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentRes = i;
        }
        resUI.AddOptions(res);
        resUI.value = currentRes;
        resUI.RefreshShownValue();

        if (PlayerPrefs.HasKey(prefPrefix + "Fullsreen"))
        {
            int state = PlayerPrefs.GetInt(prefPrefix + "Fullsreen");
            if (state == 1)
                Screen.fullScreen = true;
            else
                Screen.fullScreen = false;
        }
    }

    public int GetQuality()
    {
        if (PlayerPrefs.HasKey(prefPrefix + "Quality"))
        {
            quality = PlayerPrefs.GetInt(prefPrefix + "Quality");
            return quality;
        }
        else
        {
            return defaultQuality;
        }
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        quality = index;
        PlayerPrefs.SetInt(prefPrefix + "Quality", quality);
    }

    public void SetFullscreen(bool state)
    {
        Screen.fullScreen = state;

        if (state)
            PlayerPrefs.SetInt(prefPrefix + "Fullsreen", 1);
        else
            PlayerPrefs.SetInt(prefPrefix + "Fullsreen", 0);
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(prefPrefix + "Resolution", resIndex);
    }
}
