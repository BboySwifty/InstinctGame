using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private Inventory[] inventories;
    private int activeInventory = -1;
    private Player player;

    public event EventHandler<EventArgs> ItemsChanged;

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
        inventories = new Inventory[2];
        inventories[0] = new Inventory(2);
        inventories[1] = new Inventory(6);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public Inventory[] getInventories()
    {
        return inventories;
    }

    public void PickupItem(int inventoryIndex, Item item)
    {
        Inventory inv = inventories[inventoryIndex];
        if (activeInventory != -1 && activeInventory != inventoryIndex)
        {
            if (!inv.IsFull())
            {
                inv.AddItem(item);
                item.gameObject.SetActive(false);
            }
            else
            {
                // Can't
                return;
            }
        }
        else
        {
            activeInventory = inventoryIndex;
            if (inv.activeItem != -1 && inv.IsFull())
            {
                inv.GetActiveItem().Drop();
                inv.SetItem(inv.activeItem, item);
                SelectItem(inventoryIndex, inv.activeItem);
            }
            else if (inv.activeItem != -1)
            {
                inv.AddItem(item);
                item.gameObject.SetActive(false);
            }
            else if (!inv.IsFull())
            {
                inv.AddItem(item);
                item.gameObject.SetActive(true);
                SelectItem(inventoryIndex, inv.GetItems().Count - 1);
            }
            else
            {
                // Can't
                return;
            }
        }
        InvokeItemChanged();
    }

    public Inventory getGunInventory()
    {
        return inventories[0];
    }

    public Inventory getItemInventory()
    {
        return inventories[1];
    }

    public void UseActiveItem()
    {
        Item item = GetActiveItem();
        if (item != null)
            item.Use();
    }

    public void RemoveItem(int index, Item item)
    {
        inventories[index].RemoveItem(item);
        InvokeItemChanged();
    }

    public void SelectItem(int inventoryIndex, int index)
    {
        if (inventoryIndex == activeInventory && inventories[inventoryIndex].activeItem == index)
        {
            DeselectAllItems();
            activeInventory = -1;
            player.SetState(0);
        }
        else
        {
            DeselectAllItems();
            inventories[inventoryIndex].SelectItem(index);
            activeInventory = inventoryIndex;
            player.SetState(inventoryIndex + 1);
        }
        InvokeItemChanged();
    }

    public void DeselectAllItems()
    {
        inventories[0].DeselectCurrentItem();
        inventories[1].DeselectCurrentItem();
    }

    public Item GetActiveItem()
    {
        if (activeInventory == -1)
            return null;
        return inventories[activeInventory].GetActiveItem();
    }

    public void InvokeItemChanged()
    {
        if (ItemsChanged != null)
        {
            ItemsChanged.Invoke(this, new EventArgs());
        }
    }
}
