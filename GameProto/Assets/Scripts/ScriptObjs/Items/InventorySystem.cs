using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySystem
{
    [SerializeField]
    private Dictionary<Item, InventorySlot> itemDictionary;
    public List<InventorySlot> inventory;

    /*
    private void Awake()
    {
        inventory = new List<InventorySlot>();
        itemDictionary = new Dictionary<Item, InventorySlot>();
    }
    */

    public InventorySystem()
    {
        inventory = new List<InventorySlot>();
        itemDictionary = new Dictionary<Item, InventorySlot>();
    }

    public InventorySlot Get(Item reference)
    {
        if (itemDictionary.TryGetValue(reference, out InventorySlot value))
        {
            return value;
        }
        return null;
    }

    public void Add(Item reference)
    {
        if(itemDictionary.TryGetValue(reference, out InventorySlot value))
        {
            value.addItem();
        }
        else
        {
            InventorySlot newItem = new InventorySlot(reference);
            inventory.Add(newItem);
            itemDictionary.Add(reference, newItem);
        }
    }

    public void Remove(Item reference)
    {
        if(itemDictionary.TryGetValue(reference, out InventorySlot value))
        {
            value.removeItem();

            if(value.invenSize <= 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(reference);
            }
        }
    }
}
