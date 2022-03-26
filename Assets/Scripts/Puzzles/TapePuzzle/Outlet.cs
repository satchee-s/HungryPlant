using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Outlet : MonoBehaviour
{
    public TapePuzzle puzzle;
    public UnityEvent interact;

    private void OnMouseDown()
    {
        if (!puzzle.powered)
        {
            interact.Invoke();
        }
    }
}
