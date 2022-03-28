using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : Puzzle
{
    public DoorController controller;
    [SerializeField] float requriedTime;
    float timer = 0f;
    [SerializeField] bool startPuzzle = false;
    InventoryManager manager;
    Item item;

    bool inRange = false;
    bool items = false;
    [SerializeField] Slider slider;
    public bool completed;

    private void Start()
    {
        slider.value = timer;
        slider.maxValue = requriedTime;
    }

    public override void ExecutePuzzle()
    {
        base.ExecutePuzzle();
    }

    public void BarricadeDoor()
    {
        if (controller != null)
        {
            startPuzzle = true;

            controller.isNotBarricaded = false;
            Debug.Log("Door has been barricaded");
        }        
        //run animation
    }

    private void Update()
    {
        if (inRange && items)
        {
            slider.gameObject.SetActive(true);
            if (Input.GetMouseButton(1))
            {
                timer += Time.deltaTime;
                slider.value = timer;
                Debug.Log(timer);
            }
            if (timer >= requriedTime)
            {
                Debug.Log("Pressed button for enough time");
                taskCompleted.Invoke();
                startPuzzle = false;
            }
        }
        else
            slider.gameObject.SetActive(false);

    }

    public void SetTrigger(bool range)
    {
        inRange = range;
        items = CheckItems();
    }
}
