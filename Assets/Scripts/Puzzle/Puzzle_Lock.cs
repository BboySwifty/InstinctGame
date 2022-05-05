using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Lock : Puzzle
{
    public CharUpDown[] locks = new CharUpDown[4];
    public RandomHint[] clues = new RandomHint[4];

    [HideInInspector]
    public string code = "";

    void Start()
    {
        foreach (RandomHint hint in clues)
        {
            code += hint.value;
        }
    }

    protected override bool IsSolved()
    {
        string currentCode = "";
        foreach (CharUpDown cud in locks)
        {
            currentCode += cud.GetValue();
        }
        return currentCode == code;
    }

    public void Unlock()
    {
        if(IsSolved())
            InvokeOpenContainer();
    }
}
