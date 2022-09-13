using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public ItemData requiredItem;
    [SerializeField] string UnsuccessfulInteractionMessage;
    [SerializeField] string SuccessfulInteractionMessage;

    public abstract void Use();

    public bool AttemptInteraction(Item item)
    {
        if (item != null && CanInteractWith(item))
        {
            UIManager.Instance.BroadcastMessageToUser(SuccessfulInteractionMessage);
            Use();
            InventoryManager.Instance.ConsumeCurrentItem();
            return true;
        }
        else
        {
            UIManager.Instance.BroadcastMessageToUser(UnsuccessfulInteractionMessage);
            return false;
        }
    }

    private bool CanInteractWith(Item item)
    {
        return item.itemData == requiredItem;
    }
}
