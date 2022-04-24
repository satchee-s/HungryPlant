using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Range(0, 100)]float batteryCharge;
    [SerializeField]float degredationRate = .01f;

    public Gradient uiColorRange;
    public Color uiColor;
    public Renderer ui;

    bool toggled;
    public Light lightObject;
    public float changeRate;
    float t;
    public float maxIntensity;
    public float currentIntensity;
    public GameObject[] lightObjects;
    
    public PlayerMovement playerMovement;
    [Range(0, 50)]public float flashBangCost;

    AudioSource sound;

    // Start is called before the first frame update
    void Awake()
    {
        batteryCharge = 100;
        toggled = false;
        //maxIntensity = lightObject.intensity;
        //lightObject.intensity = 0;
        ToggleLight(toggled);
        sound = GetComponent<AudioSource>();
        ui.material.EnableKeyword("_EMISSION");
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (toggled)
                toggled = false;
            else
                toggled = true;
            t = 0;
            sound.Play();
            ToggleLight(toggled);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            FlashBang();
        }

        //LightAmount();
        //lightObject.intensity = currentIntensity;

        if (toggled)
        {
            batteryCharge -= degredationRate * Time.deltaTime;
            updateUI();
        }

        if (batteryCharge <= 0)
        {
            if (toggled)
            {
                toggled = false;
                ToggleLight(toggled);
            }
        }
    }

    void ToggleLight(bool state)
    {
        lightObject.enabled = state;

        for (int i = 0; i < lightObjects.Length; i++)
            lightObjects[i].SetActive(state);        
    }    

    void FlashBang()
    {
        if ((batteryCharge - flashBangCost) > 0)
        {
            toggled = true;
            ToggleLight(toggled);            
            playerMovement.FlashBang();
            batteryCharge -= flashBangCost;
            updateUI();
        }        
    }

    public void AddCharge(float charge)
    {
        if ((batteryCharge + charge) < 100)
        {
            batteryCharge += charge;
        }
        else
        {
            batteryCharge = 100;
        }
    }

    void LightAmount()
    {
        t += Time.deltaTime * changeRate;
        if (toggled && lightObject.intensity < maxIntensity)
        {
            currentIntensity = Mathf.Lerp(lightObject.intensity, maxIntensity, t);            
        }
        else if (!toggled && lightObject.intensity > 0)
        {
            currentIntensity = Mathf.Lerp(lightObject.intensity, 0, t);
        }
    }

    void updateUI()
    {
        uiColor = uiColorRange.Evaluate(batteryCharge/100);
        //ui.material.color = uiColor;
        if (toggled)
            ui.material.SetColor("_EmissionColor", uiColor * 1.5f);
        else
        {
            ui.material.color = Color.black;
            ui.material.SetColor("_EmissionColor", Color.black * 1.5f);
        }            
    }
}
