using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Objective
{
    public string name { get; private set; }
    public string description { get; private set; }
    public bool completed;
    public GameObject ui { get; private set; }

    public Objective(string name, string description, GameObject ui)
    {
        this.name = name;
        this.description = description;
        completed = false;
        this.ui = ui;
    }
}

public class ObjectiveSystem : MonoBehaviour
{
    public GameObject objectivePrefab;
    public GameObject uiPanel;
    List<Objective> objectives;

    void Start()
    {
        objectives = new List<Objective>();
    }

    public void AddObjective(string name, string description)
    {
        
        GameObject uiObj = Instantiate(objectivePrefab);
        uiObj.GetComponent<Text>().text = description;
        uiObj.transform.SetParent(uiPanel.transform, false);
        uiObj.name = name;

        Objective objective = new Objective(name, description, uiObj);
        objectives.Add(objective);
    }

    public void CompleteObjective(string name)
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].name == name)
            {
                objectives[i].completed = true;
                string strikethrough = "";
                for (int j = 0; j < objectives[i].description.Length / 2; j++)
                    strikethrough += "—";
                objectives[i].ui.transform.GetChild(0).GetComponent<Text>().text = strikethrough;
            }
        }
    }
}