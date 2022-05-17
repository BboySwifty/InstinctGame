using System;
using UnityEngine;

public class PuzzleContainer : Examine
{
    private Puzzle _puzzle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _puzzle = GetComponent<Puzzle>();
        Subscribe();
    }

    private void Subscribe()
    {
        _puzzle.OpenContainer += Open;
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
