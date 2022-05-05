using System;
using UnityEngine;

public class PuzzleContainer : Examine
{
    public Puzzle puzzle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Subscribe();
    }

    private void Subscribe()
    {
        puzzle.OpenContainer += Open;
    }

    public virtual void Open(object sender, EventArgs e)
    {
        Destroy(gameObject);
        /*
        panel.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
        */
    }
}
