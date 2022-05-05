using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : Actor
{
    public GameObject healthBar;
    public GameObject interactionUI;
    public GameObject legs;

    public float score = 0;
    public int state = 0; // States : 0 = Nothing, 1 = Gun, 2 = Item
    public bool isWalking = false;

    private Interactable nearbyObject = null;
    private Animator legsAnimator;

    private void Start()
    {
        currentHealth = Health;
        rb = gameObject.GetComponent<Rigidbody2D>();
        legsAnimator = legs.GetComponent<Animator>();
    }

    private void Update()
    {
        legsAnimator.SetBool("Walking", isWalking);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void Move(Vector2 movement)
    {
        if (!knockedBack && movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

            /* Rotate the legs */
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            legs.transform.rotation = Quaternion.Slerp(legs.transform.rotation, Quaternion.Euler(0, 0, angle + 180), turnSpeed * Time.deltaTime);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void AddScore(float score)
    {
        this.score += score;
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    public Interactable GetNearbyObject()
    {
        return nearbyObject;
    }

    public void SetNearbyObject(Interactable nearbyObject)
    {
        this.nearbyObject = nearbyObject;
        interactionUI.SetActive(nearbyObject != null);
    }

    public void UseObject()
    {
        Item activeItem = InventoryManager.Instance.GetActiveItem();
        if (activeItem == null)
            InteractNearbyObject();
        else if (activeItem.GetType() == typeof(Flashlight))
            InventoryManager.Instance.UseActiveItem();
        else
            InteractNearbyObject();
    }

    public void InteractNearbyObject()
    {
        if (nearbyObject == null || !(nearbyObject is Usable))
            return;

        Usable usableObject = (Usable)nearbyObject;
        if (usableObject.IsInteractable())
        {
            usableObject.Interact();
            InventoryManager.Instance.UseActiveItem();
        }
    }

    public void PickupItem()
    {
        if (nearbyObject == null || !(nearbyObject is Item))
            return;
        nearbyObject.Interact();
    }

    public void SetWalkAnimation(bool value)
    {
        legsAnimator.SetBool("Walking", value);
    }
}
