using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Text percentageText;
    [SerializeField] Text getOut;

    //[SerializeField] GameObject outsideTrigger;

    [SerializeField] float drainSpeed = 0.5f;

    public bool hasGasCan;

    private void Start()
    {
        slider.value = 1;
    }

    private void Update()
    {
        percentageText.text = Mathf.RoundToInt(slider.value * 100) + "%";

        if (Input.GetMouseButton(0) && (hasGasCan = true))
        {
            PourGas();
        }

        if (slider.value <= 0)
        {
            slider.gameObject.SetActive(false);
            percentageText.gameObject.SetActive(false);
            getOut.gameObject.SetActive(true);
            hasGasCan = false;
            //Instantiate(outsideTrigger);
        }
    }
    void PourGas()
    {
        percentageText.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        slider.value -= drainSpeed * Time.deltaTime;
        // Animation logic
    }
}
