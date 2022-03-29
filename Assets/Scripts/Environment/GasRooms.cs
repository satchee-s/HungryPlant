using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasRooms : Puzzle
{
    Gas main;

    [SerializeField] Slider slider;
    [SerializeField] Text percentageText;
    [SerializeField] float drainSpeed = .5f;

    bool inArea;
    public bool complete;

    private void Update()
    {
        percentageText.text = Mathf.RoundToInt(slider.value * 100) + "%";

        if (inArea && !complete)
        {
            if (Input.GetMouseButton(0))
            {
                if (CheckItems())
                    PourGas();
            }

            if (slider.value <= 0)
            {
                slider.gameObject.SetActive(false);
                percentageText.gameObject.SetActive(false);
                taskCompleted.Invoke();
                complete = true;
                slider.value = 1;
            }
        }       
    }
    void PourGas()
    {
        percentageText.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        slider.value -= drainSpeed * Time.deltaTime;
        // Animation logic
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inArea = false;
            slider.value = 1;
            slider.gameObject.SetActive(false);
            percentageText.gameObject.SetActive(false);
        }
    }
}
