using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Interactable
{

    public string SuccessfulInteractionMessage;
    public string UnsuccessfulInteractionMessage;
    public int requiredItemID;

    public abstract void Use();

    public override void Interact()
    {
        UIUpdate.Instance.BroadcastMessageToUser(SuccessfulInteractionMessage);
        Use();
    }

    public bool IsInteractable()
    {
        Item activeItem = InventoryManager.Instance.GetActiveItem();
        bool result = (requiredItemID == -1 || (activeItem != null && activeItem.id == requiredItemID));
        if(!result)
            UIUpdate.Instance.BroadcastMessageToUser(UnsuccessfulInteractionMessage);
        return result;
    }
}
