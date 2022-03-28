using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Text percentageText;
    [SerializeField] Text getOut;

    [SerializeField] GameObject frontDoor;

    [SerializeField] float drainSpeed = 0.5f;

    public bool hasGasCan;
    bool allRoomsComplete;

    public List<GasRooms> rooms;

    private void Start()
    {
        slider.value = 1;
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
            Destroy(frontDoor);

            if(getOut != null)
            {
                getOut.gameObject.SetActive(true);
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
}
