using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public int id;
    public int uses = 1;
    public string itemName;
    public string description;
    public Sprite inventoryImage;

    protected Player player;
    protected Collider2D _collider;
    protected int inventoryIndex = 1;

    public virtual void Start()
    {
        _collider = GetComponent<Collider2D>();
        player = Globals.Instance.player;
    }

    public override void Interact()
    {
        UIUpdate.Instance.BroadcastMessageToUser("Picked up " + itemName);
        transform.SetParent(player.gameObject.transform.Find("ItemHolder"));
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.identity;
        _collider.enabled = false;
        gameObject.SetActive(false);
        InventoryManager.Instance.PickupItem(inventoryIndex, this);
    }
    public void Drop()
    {
        
    }

    public virtual void Use()
    {
        uses--;
        if (uses <= 0)
        {
            InventoryManager.Instance.RemoveItem(1, this);
            Destroy(gameObject);
        }
    }
}
