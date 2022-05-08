using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    public event EventHandler OpenContainer;

    protected abstract bool IsSolved();
    
    [Header("Hints")]
    public string hintFolderPath;
    public Hint[] hints;

    [HideInInspector]
    public string value;

    protected override void Start()
    {
        GenerateRandomHint();
    }

    protected override void GenerateRandomHint()
    {
        value = "";
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderPath);
        foreach (Hint hint in hints)
        {
            int index = Random.Range(0, sprites.Length);
            hint.SetSprite(sprites[index]);
            hint.SetExamineImage(sprites[index]);
            value += sprites[index].name.Substring(sprites[index].name.IndexOf('_') + 1);
        }
    }

    protected void InvokeOpenContainer()
    {
        OpenContainer?.Invoke(this, EventArgs.Empty);
    }
}
