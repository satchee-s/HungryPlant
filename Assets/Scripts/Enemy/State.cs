using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected AIManager aiManager;
    //protected Transform player;
    protected Pathfinding pathfinding;
    protected Transform plant;

    private void Start()
    {
        aiManager = FindObjectOfType<AIManager>();
        pathfinding = FindObjectOfType<Pathfinding>();
        plant = GameObject.Find("Plant").GetComponent<Transform>();
    }

    public abstract void SetBehaviour();
}
