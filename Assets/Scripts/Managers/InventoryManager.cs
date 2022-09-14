using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform PlayerItems;
    [SerializeField] Transform EnvironmentItems;
    private Inventory inventory;
    private Item activeItem;
    private InventoryType activeInvType;

    public event EventHandler<ItemsChangedEventArgs> ItemsChanged;

    #region Singleton
    public static InventoryManager Instance { get; private set; }

    public void CreateInstance()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CreateInstance();
        inventory = new Inventory(2, 6);
    }

    private void Start()
    {
        InputManager.Instance.OnMouse += UseItem;
        InputManager.Instance.OnMouseDown += UseItemDown;
    }

    public Item GetActiveItem()
    {
        return activeItem;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void ConsumeCurrentItem()
    {
        inventory.RemoveItem(activeItem);
        if (activeItem is IConsumable)
            (activeItem as IConsumable).Consume();
        InvokeItemsChanged();
    }

    public void DropCurrentItem()
    {
        inventory.RemoveItem(activeItem);
        activeItem.transform.SetParent(EnvironmentItems);
        activeItem.GetComponent<Collider2D>().enabled = true;
        activeItem = null;
        activeInvType = InventoryType.None;
        InvokeItemsChanged();
    }

    public void PickupItem(Item item)
    {
        InventoryType invType = item is Gun ? InventoryType.Gun : InventoryType.Item;
        int itemIndex = inventory.AddItem(item);

        if (itemIndex == -1 && invType != activeInvType)
            // Can't pickup
            return;

        if (itemIndex != -1 && activeItem == null) // item was added AND not wielding anything
        {
            SelectItem(invType, itemIndex);
        }
        else if(itemIndex != -1 && activeItem != null) // item was added AND wielding something
        {
            item.gameObject.SetActive(false);
        }
        else // item was not added AND wielding something
        {
            int activeItemIndex = inventory.GetIndexOf(activeItem);
            ReplaceItem(item, activeItemIndex);
            SelectItem(invType, activeItemIndex);
        }
        AddItemToItemHolder(item);

        UIManager.Instance.BroadcastMessageToUser("Picked up " + item.itemData.itemName);

        InvokeItemsChanged();
    }

    public void UseItem(object state, EventArgs e)
    {
        if(activeItem is IUsable)
           (activeItem as IUsable).Use();
    }

    public void UseItemDown(object state, EventArgs e)
    {
        if (activeItem is IUsable)
            (activeItem as IUsable).UseDown();
    }

    public void SelectItem(InventoryType invType, int index)
    {
        Item item = inventory.GetItemAtIndex(invType, index);
        bool isSameItem = activeItem == item;
        DeselectCurrentItem();

        if(isSameItem && invType == InventoryType.Gun)
        {
            List<Gun> guns = inventory.GetGuns();
            Gun placeholder = guns[0];
            guns[0] = guns[1];
            guns[1] = placeholder;
            item.gameObject.SetActive(false);
            guns[0].gameObject.SetActive(true);
            activeItem = guns[0];
        }
        else if (isSameItem || item == null)
        {
            activeInvType = InventoryType.None;
        }
        else
        {
            activeInvType = invType;
            item.gameObject.SetActive(true);
            activeItem = item;
        }

        InvokeItemsChanged();
    }

    private void AddItemToItemHolder(Item item)
    {
        item.transform.SetParent(PlayerItems);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.identity;
    }

    private void DeselectCurrentItem()
    {
        if (activeItem == null)
            return;

        activeItem.gameObject.SetActive(false);
        activeItem = null;
    }

    private void ReplaceItem(Item item, int itemIndex)
    {
        inventory.SetItemAtIndex(item, itemIndex);
        activeItem.transform.SetParent(EnvironmentItems);
        activeItem.GetComponent<Collider2D>().enabled = true;
        activeItem = null;
        activeInvType = InventoryType.None;
        InvokeItemsChanged();
    }

    private void InvokeItemsChanged()
    {
        ItemsChanged?.Invoke(this, new ItemsChangedEventArgs(activeItem));
    }
}

public class ItemsChangedEventArgs : EventArgs
{
    public Item NewItem { get; }

    public ItemsChangedEventArgs(Item newItem)
    {
        NewItem = newItem;
    }
}
