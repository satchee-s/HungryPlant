using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2AnimationEventTrigger : MonoBehaviour
{

    public GreenhouseDoorTrigger eventHolder;

    public void SlowTime()
    {
        eventHolder.SlowTime();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
