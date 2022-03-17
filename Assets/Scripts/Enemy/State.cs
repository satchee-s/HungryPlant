using UnityEngine;

public abstract class State
{
    protected AIManager AIManager;
    protected Transform player;

    public abstract void SetBehaviour();
}
