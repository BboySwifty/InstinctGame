using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_SquareLock : Puzzle
{

    public UIToggle[] toggles;
    public RandomHint hint;

    [HideInInspector]
    public string code = "";

    // Start is called before the first frame update
    void Start()
    {
        code = hint.value;
    }

    protected override bool IsSolved()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (code[i] != (toggles[i].IsOn() ? '1' : '0'))
                return false;
        }
        return true;
    }

    public void Unlock()
    {
        if (IsSolved())
            InvokeOpenContainer();
    }
}
