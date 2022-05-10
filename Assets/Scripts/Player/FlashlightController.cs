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
    public float flashMultiplier;
    float flashIntensity;
    public float currentIntensity;
    public GameObject[] lightObjects;
    
    public PlayerMovement playerMovement;
    public AudioSource flashSound;
    public float flashBangCost;
    bool flashOn;
    public float flashDuration;
    float flashTime;

    Ray ray;
    RaycastHit hit;
    public Vector2 intensityRange;
    AnimationCurve intensityFalloff;
    public float lightMaxRange;
    public LayerMask playerLayer;

    AudioSource sound;

    // Start is called before the first frame update
    void Awake()
    {
        batteryCharge = 100;
        toggled = false;     

        ToggleLight(toggled);
        sound = GetComponent<AudioSource>();
        ui.material.EnableKeyword("_EMISSION");
        updateUI();
        
        intensityFalloff = new AnimationCurve();
        intensityFalloff.AddKey(0, intensityRange.x);
        intensityFalloff.AddKey(1, intensityRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.right);
        if (Physics.Raycast(ray, out hit, lightMaxRange))
        {
            maxIntensity = intensityFalloff.Evaluate(hit.distance / lightMaxRange);
            flashIntensity = maxIntensity * flashMultiplier;
            //lightObject.range = hit.distance * 1.5f;
        }

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
            flashSound.Play();
            flashOn = true;
        }        

        LightAmount();
        lightObject.intensity = currentIntensity;
        if (flashOn)
        {
            flashTime += Time.deltaTime;
            if (flashTime > flashDuration)
            {
                flashOn = false;
                flashTime = 0;
            }
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
        if ((toggled && lightObject.intensity != maxIntensity) && !flashOn)
        {
            currentIntensity = Mathf.Lerp(lightObject.intensity, maxIntensity, t);
        }
        else if ((toggled && lightObject.intensity != flashIntensity) && flashOn)
        {
            currentIntensity = Mathf.Lerp(lightObject.intensity, flashIntensity, flashTime/flashDuration);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, hit.point);
        Gizmos.color = uiColor;
        Gizmos.DrawRay(ray);
        
    }
}
