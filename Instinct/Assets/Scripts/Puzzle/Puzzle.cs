using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    public event EventHandler OpenContainer;

    protected abstract bool IsSolved();

    protected void InvokeOpenContainer()
    {
        OpenContainer?.Invoke(this, EventArgs.Empty);
    }
}
