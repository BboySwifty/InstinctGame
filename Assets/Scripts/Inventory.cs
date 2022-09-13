using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    private List<Gun> guns;
    private List<Item> items;

    public Inventory(int gunsSize, int itemsSize)
    {
        items = new List<Item>
        {
            Capacity = itemsSize
        };

        guns = new List<Gun>
        {
            Capacity = gunsSize
        };
    }

    public List<Gun> GetGuns()
    {
        return guns;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public int AddItem(Item item)
    {
        if (!HasSpace(item))
            return -1;

        if (item is Gun)
        {
            guns.Add(item as Gun);
            return guns.IndexOf(item as Gun);
        }
        else
        {
            items.Add(item);
            return items.IndexOf(item);
        }
    }

    public bool HasSpace(Item item)
    {
        return item is Gun ? guns.Count < guns.Capacity : items.Count < items.Capacity;
    }

    public void SetItemAtIndex(Item item, int index)
    {
        if (item is Gun)
            guns[index] = item as Gun;
        else
            items[index] = item;
    }

    public void RemoveItem(Item item)
    {
        if (item is Gun)
            guns.Remove(item as Gun);
        else
            items.Remove(item);
    }

    public int GetIndexOf(Item item)
    {
        return item is Gun ? guns.IndexOf(item as Gun) : items.IndexOf(item);
    }

    public Item GetItemAtIndex(InventoryType invType, int index)
    {
        Item item = null;
        if (invType == InventoryType.Item && index < items.Count)
            item = items[index];
        else if (invType == InventoryType.Gun && index < guns.Count)
            item = guns[index];
        return item;
    }

    public int GetItemCount()
    {
        return items.Count;
    }

    public int GetGunCount()
    {
        return guns.Count;
    }
}

public enum InventoryType
{
    None,
    Item,
    Gun
}