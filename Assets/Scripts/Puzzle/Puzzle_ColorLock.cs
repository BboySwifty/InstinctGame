using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_ColorLock : Puzzle
{

    public ColorToggle[] colorToggles;

    protected override bool IsSolved()
    {
        string currentCode = "";
        foreach (ColorToggle ct in colorToggles)
        {
            currentCode += ct.GetCodeValue();
        }
        return currentCode == code;
    }
}
