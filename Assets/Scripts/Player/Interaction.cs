using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject interactionUI;

    private List<IPickupable> nearbyItems;
    private List<InteractableObject> nearbyObjects;

    void Awake()
    {
        nearbyItems = new List<IPickupable>();
        nearbyObjects = new List<InteractableObject>();
    }

    private void Start()
    {
        InputManager.Instance.OnPickup += PickupItem;
        InputManager.Instance.OnMouseDown += InteractWithObject;
    }

    public IPickupable GetNearbyObject()
    {
        return nearbyItems.Count > 0 ? nearbyItems[0] : null;
    }

    public void PickupItem(object state, EventArgs e)
    {
        if (nearbyItems.Count == 0)
            return;
        nearbyItems[0].Pickup();
    }

    public void InteractWithObject(object state, EventArgs e)
    {
        if (nearbyObjects.Count == 0)
            return;
        nearbyObjects[0].AttemptInteraction(InventoryManager.Instance.GetActiveItem());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPickupable interactable = collision.GetComponent<IPickupable>();
        if (interactable != null)
        {
            AddNearbyItem(interactable);
            return;
        }

        InteractableObject iObject = collision.GetComponent<InteractableObject>();
        if (iObject != null)
        {
            nearbyObjects.Add(iObject);
            return;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        IPickupable interactable = collision.GetComponent<IPickupable>();
        if (interactable != null)
        {
            RemoveNearbyItem(interactable);
            return;
        }

        InteractableObject iObject = collision.GetComponent<InteractableObject>();
        if (iObject != null)
        {
            nearbyObjects.Remove(iObject);
            return;
        }
    }

    private void AddNearbyItem(IPickupable nearbyItem)
    {
        nearbyItems.Add(nearbyItem);
        interactionUI.SetActive(true);
    }

    private void RemoveNearbyItem(IPickupable nearbyItem)
    {
        nearbyItems.Remove(nearbyItem);
        interactionUI.SetActive(nearbyItems.Count > 0);
    }
}
