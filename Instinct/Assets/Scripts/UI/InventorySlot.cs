using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public TextMeshProUGUI itemNameLabel;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            itemNameLabel.text = item.itemName;
            icon.sprite = item.inventoryImage;
            icon.enabled = true;
        }
        else
        {
            ClearItem();
        }

    }

    public void ClearItem()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemNameLabel.text = "";
    }
}
