using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : PuzzleContainer
{
    public GameObject dropPrefab;
    public ItemData dropData;
    public Transform dropTransform;
    public Sprite openedSprite;
    public SpriteRenderer spriteRenderer;

    public override void Start()
    {
        base.Start();
    }

    public override void Open(object sender, EventArgs e)
    {
        spriteRenderer.sprite = openedSprite;
        dropPrefab.GetComponent<Item>().itemData = dropData;
        Instantiate(dropPrefab, dropTransform.position, Quaternion.identity, Globals.Instance.pickupParent);
        panel.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }
}
