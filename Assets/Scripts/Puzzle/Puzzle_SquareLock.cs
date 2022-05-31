using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_SquareLock : Puzzle
{

    public UIToggle[] toggles;

    protected override bool IsSolved()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (code[i] != (toggles[i].IsOn() ? '1' : '0'))
                return false;
        }
        return true;
    }
}
