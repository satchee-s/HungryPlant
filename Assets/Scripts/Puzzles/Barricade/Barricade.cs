using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : Puzzle
{
    public DoorController controller;
    [SerializeField] float requriedTime;
    float timer = 0f;
    public bool startPuzzle = false;

    bool inRange = false;
    bool items = false;
    [SerializeField] Slider slider;
    public bool completed;

    bool itemChecked;
    [SerializeField] AudioClip[] hammerSounds;
    [SerializeField] AudioSource audioSource;
    public float hammerInterval = 0.5f;
    float hammerTime;

    private void Start()
    {
        inventoryManager = GameObject.Find("PlayerParent").GetComponent<InventoryManager>();
        subtitle = FindObjectOfType<SubtitleSystem>();
        slider.value = timer;
        slider.maxValue = requriedTime;
        slider.gameObject.SetActive(false);
        hammerTime = 0f;
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

    void CheckForItems()
    {
        if (!itemChecked)
        {
            items = CheckItems();
            itemChecked = true;
        }
    }

    override public void ExecutePuzzle()
    {
        CheckForItems();
        if (inRange && items && !completed)
        {
            slider.gameObject.SetActive(true);
            timer += Time.deltaTime;
            slider.value = timer;

            if (!audioSource.isPlaying && hammerTime >= hammerInterval)
            {
                audioSource.transform.position = gameObject.transform.position;
                int index = Random.Range(0, hammerSounds.Length);
                audioSource.PlayOneShot(hammerSounds[index]);
                hammerTime = 0;
            }
            hammerTime += Time.deltaTime;
            //Debug.Log(timer);

            if (timer >= requriedTime)
            {
                //Debug.Log("Pressed button for enough time");
                taskCompleted.Invoke();
                for (int i = 0; i < consumeItems.Count; i++)
                {
                    ConsumeItem(consumeItems[i]);
                }
                completed = true;
                startPuzzle = false;                
            }
        }
        if (!inRange || !items || completed)
            slider.gameObject.SetActive(false);

    }

    public void SetTrigger(bool range)
    {
        if (startPuzzle)
        {
            inRange = range;
            if (!inRange)
            {
                itemChecked = false;
                slider.gameObject.SetActive(false);
            }
        }        
    }
}
