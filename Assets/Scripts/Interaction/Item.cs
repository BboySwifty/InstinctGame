using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable, IUsable
{
    public ItemData itemData;

    public override void Interact()
    {
        UIManager.Instance.BroadcastMessageToUser("Picked up " + itemData.itemName);
        InventoryManager.Instance.PickupItem(this);
    }

    public virtual void Use()
    {
        Interactable nearbyObject = Player.Instance.GetNearbyObject();
        if (nearbyObject is Usable && (nearbyObject as Usable).IsInteractable(itemData))
        {
            (nearbyObject as Usable).Interact();
            InventoryManager.Instance.DropCurrentItem();
            Destroy(gameObject);
        }
    }

    public virtual void UseDown()
    {
    }
}
