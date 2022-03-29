using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    [SerializeField] Slider slider;
    public string CompletedText;
    bool triggered;
    //[SerializeField] Text percentageText;
    //[SerializeField] Text getOut;

    public GameObject outside;
    [SerializeField] GameObject frontDoor;
    SubtitleSystem subtitleSystem;
    [SerializeField] float drainSpeed = 0.5f;

    public bool hasGasCan;
    bool allRoomsComplete;

    public List<GasRooms> rooms;

    private void Start()
    {
        outside.SetActive(false);
        subtitleSystem = FindObjectOfType<SubtitleSystem>();
        slider.value = 1;
        triggered = false;
    }

    private void Update()
    {

        int completeCount = 0;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].complete)
            {
                completeCount++;
            }
        }
        if (completeCount == rooms.Count)
        {
            allRoomsComplete = true;
            if (!triggered)
            {
                subtitleSystem.DisplaySubtitle(CompletedText);
                triggered = true;
                outside.SetActive(true);
            }
           
            //Destroy(frontDoor);

            //if(getOut != null)
            //{
            //    getOut.gameObject.SetActive(true);
            //}         
        }
    }
    void PourGas()
    {
        //percentageText.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        slider.value -= drainSpeed * Time.deltaTime;
        // Animation logic
    }
}
