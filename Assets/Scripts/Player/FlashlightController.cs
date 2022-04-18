using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    bool obtained;

    [Range(0, 100)]float batteryCharge;
    [SerializeField]float degredationRate = .01f;

    public Gradient uiColorRange;
    public Color uiColor;
    public Renderer ui;

    bool toggled;
    public Light light;
    public GameObject[] lightObjects;
    
    public PlayerMovement playerMovement;
    [Range(0, 50)]public float flashBangCost;

    // Start is called before the first frame update
    void Start()
    {
        obtained = false;
        batteryCharge = 100;
        toggled = false;
        ToggleLight(toggled);

        ui.material.EnableKeyword("_EMISSION");
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (obtained)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (toggled)
                    toggled = false;
                else
                    toggled = true;
                ToggleLight(toggled);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                FlashBang();
            }

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
    }

    void ToggleLight(bool state)
    {
        light.enabled = state;
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

    public void ObtainedFlashlight()
    {
        obtained = true;
    }
}
