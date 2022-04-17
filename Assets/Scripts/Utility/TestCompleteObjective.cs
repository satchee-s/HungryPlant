using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompleteObjective : MonoBehaviour
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
            objectives.CompleteObjective("TestObjective");
        }
    }
}
