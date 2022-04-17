using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectiveTrigger : MonoBehaviour
{

    public ObjectiveSystem objectives;
    bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        objectives = FindObjectOfType<ObjectiveSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !triggered)
        {
            objectives.AddObjective("TestObjective", "Go to the yellow cube to complete");
            triggered = true;
        }
    }
}
