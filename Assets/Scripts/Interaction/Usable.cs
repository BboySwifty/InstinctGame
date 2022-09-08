using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Interactable
{
    [SerializeField] string SuccessfulInteractionMessage;
    [SerializeField] string UnsuccessfulInteractionMessage;
    [SerializeField] ItemData requiredItem;

    public abstract void Use();

    public ItemData GetRequiredItem()
    {
        return requiredItem;
    }

    public override void Interact()
    {
        /*if(!IsInteractable())
            UIManager.Instance.BroadcastMessageToUser(UnsuccessfulInteractionMessage);
        else
        {
            UIManager.Instance.BroadcastMessageToUser(SuccessfulInteractionMessage);
        }*/
        Use();
    }

    public bool IsInteractable(ItemData item)
    {
        return item == requiredItem;
    }
}
