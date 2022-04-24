using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public string description;
    ObjectiveSystem system;

    private void Start()
    {
        system = FindObjectOfType<ObjectiveSystem>();
    }

    public void AddObjective()
    {
        system.AddObjective(gameObject.name, description);
    }

    public void CompleteObjective()
    {
        system.CompleteObjective(gameObject.name);
    }
}
