using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeypadButton : MonoBehaviour
{
    public string number;
    public UnityEvent keyPressed;

    Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = number.ToString();
    }

    public void PressKey()
    {
        keyPressed.Invoke();
    }
}
