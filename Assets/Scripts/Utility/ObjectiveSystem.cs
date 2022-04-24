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

    public GameObject popUp;
    Queue<Objective> popUpQueue = new Queue<Objective>();
    public Vector3 startPos;
    public float shiftAmount;
    Vector3 endPos;
    public float rate, duration;

    void Start()
    {
        objectives = new List<Objective>();
        startPos = popUp.transform.position;
        Vector3 shift = new Vector3(startPos.x + shiftAmount, startPos.y, startPos.z);
        endPos = shift;

        StartCoroutine(ObjectivePopUp());
    }

    public void AddObjective(string name, string description)
    {
        
        GameObject uiObj = Instantiate(objectivePrefab);
        uiObj.GetComponent<Text>().text = description;
        uiObj.transform.SetParent(uiPanel.transform, false);
        uiObj.name = name;

        Objective objective = new Objective(name, description, uiObj);
        objectives.Add(objective);
        popUpQueue.Enqueue(objective);
    }

    public void CompleteObjective(string name)
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].name == name)
            {
                objectives[i].completed = true;
                Strikethrough(objectives[i], true);
                popUpQueue.Enqueue(objectives[i]);
            }
        }
    }

    void Strikethrough(Objective objective, bool strike)
    {
        if (strike)
        {
            string strikethrough = "";
            for (int i = 0; i < objective.description.Length / 2; i++)
            {
                strikethrough += "—";
            }
            objective.ui.transform.GetChild(0).GetComponent<Text>().text = strikethrough;
        }
        else
        {
            objective.ui.transform.GetChild(0).GetComponent<Text>().text = "•";
        }
    }

    IEnumerator ObjectivePopUp()
    {
        while (true)
        {
            if (popUpQueue.Count > 0)
            {
                GameObject obj = Instantiate(objectivePrefab);
                Objective objective = popUpQueue.Dequeue();
                Objective temp = new Objective(objective.name, objective.description, obj);
                temp.completed = objective.completed;
                obj.GetComponent<Text>().text = temp.description;
                if (temp.completed)
                {
                    Strikethrough(temp, temp.completed);
                }
                obj.transform.SetParent(popUp.transform, false);

                while (Vector3.Distance(popUp.transform.position, endPos) > .01f)
                {
                    Vector3 pos = Vector3.Lerp(popUp.transform.position, endPos, Time.deltaTime * rate);
                    popUp.transform.position = pos;
                    yield return null;
                }
                yield return new WaitForSeconds(duration);

                while (Vector3.Distance(popUp.transform.position, startPos) > .01f)
                {
                    Vector3 pos = Vector3.Lerp(popUp.transform.position, startPos, Time.deltaTime * rate);
                    popUp.transform.position = pos;
                    yield return null;
                }

                Destroy(obj);
            }
            yield return null;
        }
    }
}