using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenChest : BreakableBox
{
    // Add 2nd variable for inventory/examine image
    public void SetDropSprite(Sprite sprite)
    {
        dropPrefab.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        dropPrefab.GetComponent<Item>().itemData.inventoryImage = sprite;
    }
}
