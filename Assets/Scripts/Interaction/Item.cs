using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class Item : MonoBehaviour, IPickupable
{
    public ItemData itemData;

    public virtual void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.holdingImage;
    }

    public void Pickup()
    {
        InventoryManager.Instance.PickupItem(this);
    }
}
