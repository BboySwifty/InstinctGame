using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Lock : Puzzle
{
    public CharUpDown[] locks;

    protected override bool IsSolved()
    {
        string currentCode = "";
        foreach (CharUpDown cud in locks)
        {
            currentCode += cud.GetValue();
        }
        return currentCode == code;
    }
}
