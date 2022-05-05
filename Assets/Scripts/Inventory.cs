using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public int activeItem = -1;

    private int inventorySize;
    private List<Item> items;

    public Inventory(int inventorySize)
    {
        this.inventorySize = inventorySize;

        items = new List<Item>
        {
            Capacity = inventorySize
        };
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void SetItem(int index, Item item)
    {
        items[index] = item;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        activeItem = -1;
    }

    public void SelectItem(int index)
    {
        if (index <= items.Count - 1)
        {
            if (activeItem != -1)
                items[activeItem].gameObject.SetActive(false);
            activeItem = index;
            items[activeItem].gameObject.SetActive(true);
        }
    }

    public void DeselectCurrentItem()
    {
        if (activeItem != -1)
        {
            items[activeItem].gameObject.SetActive(false);
            activeItem = -1;
        }
    }

    public void DropItem(int itemIndex)
    {
        items[itemIndex].Drop();
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public Item GetItemAtIndex(int index)
    {
        Item item = null;
        if (index <= items.Count - 1)
        {
            item = items[index];
        }
        return item;
    }

    public Item GetActiveItem()
    {
        Item item = null;
        if (activeItem != -1)
        {
            item = items[activeItem];
        }
        return item;
    }

    public bool IsFull()
    {
        return items.Count >= inventorySize;
    }

    public int GetItemCount()
    {
        return items.Count;
    }
}