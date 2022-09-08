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

    public Item GetActiveItem()
    {
        return activeItem;
    }

    public Inventory GetInventory()
    {
        return inventory;
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
            DropCurrentItem();
            inventory.SetItemAtIndex(item, activeItemIndex);
            SelectItem(activeInvType, activeItemIndex);
        }
        AddItemToItemHolder(item);

        InvokeItemsChanged();
    }

    public void UseItem()
    {
        if(activeItem != null)
            activeItem.Use();
    }

    public void UseItemDown()
    {
        if (activeItem != null)
            activeItem.UseDown();
    }

    public void SelectItem(InventoryType invType, int index)
    {
        Item item = inventory.GetItemAtIndex(invType, index);
        bool isSameItem = activeItem == item;
        DeselectCurrentItem();

        if (isSameItem || item == null)
        {
            activeInvType = InventoryType.None;
        }
        else
        {
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
        item.GetComponent<Collider2D>().enabled = false;
    }

    private void DeselectCurrentItem()
    {
        if (activeItem == null)
            return;

        activeItem.gameObject.SetActive(false);
        activeItem = null;
    }

    private void InvokeItemsChanged()
    {
        ItemsChanged?.Invoke(this, new ItemsChangedEventArgs(activeItem is Gun));
    }
}

public class ItemsChangedEventArgs : EventArgs
{
    public bool GunIsEquipped { get; }

    public ItemsChangedEventArgs(bool gunIsEquipped)
    {
        GunIsEquipped = gunIsEquipped;
    }
}
