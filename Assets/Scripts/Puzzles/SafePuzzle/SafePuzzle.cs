using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzle : Puzzle
{
    [SerializeField] bool startPuzzle = false;
    string code;
    string input = "";
    [SerializeField] Text writing;
    [SerializeField] Text keypad;
    void Start()
    {
        writing.gameObject.SetActive(false);
        input = "";
    }

    public void RevealCode()
    {
        GenerateCode();
        writing.gameObject.SetActive(true);
        writing.text ="" + code;
    }

    void GenerateCode()
    {
        string c = "";
        for (int i = 0; i < 4; i++)
            c += (int)Random.Range(0, 9);
        code = c;
    }

    public void KeyPressed(string number)
    {
        input += number;
        keypad.text = input;
        if (input.Length >= 4)
        {
            if (input == code)
            {
                taskCompleted.Invoke();
            }
            else
            {
                input = "";
            }
        }
    }

    public void ClearCode()
    {
        input = "";
        keypad.text = input;
    }
}
