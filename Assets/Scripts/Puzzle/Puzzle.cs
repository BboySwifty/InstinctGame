using System;
using UnityEngine;

[RequireComponent(typeof(PuzzleContainer))]
public abstract class Puzzle : MonoBehaviour
{

    public event EventHandler OpenContainer;

    protected abstract bool IsSolved();
    
    [Header("Hints")]
    public string hintFolderPath;
    public Hint[] hints;

    [HideInInspector]
    public string code;

    protected virtual void Start()
    {
        GenerateRandomHint();
    }

    protected virtual void GenerateRandomHint()
    {
        code = "";
        Sprite[] sprites = Resources.LoadAll<Sprite>(hintFolderPath);
        foreach (Hint hint in hints)
        {
            int index = UnityEngine.Random.Range(0, sprites.Length);
            hint.SetSprite(sprites[index]);
            hint.SetExamineImage(sprites[index]);
            code += sprites[index].name.Substring(sprites[index].name.IndexOf('_') + 1);
        }
    }

    protected void InvokeOpenContainer()
    {
        OpenContainer?.Invoke(this, EventArgs.Empty);
    }
}
