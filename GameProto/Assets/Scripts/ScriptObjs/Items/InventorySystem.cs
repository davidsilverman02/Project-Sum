using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySystem : MonoBehaviour
{
    private Dictionary<Item, InventorySlot> itemDictionary;
    public List<InventorySlot> inventory;
    //{ get; private set; }

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

    public string GetCount(Item reference)
    {
        if (itemDictionary.TryGetValue(reference, out InventorySlot value))
        {
            return value.invenSize.ToString();
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

    public List<InventorySlot> getConsumables()
    {
        List<InventorySlot> returnType = new List<InventorySlot>();

        for(int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].data is Consumable)
            {
                returnType.Add(inventory[i]);
            }
        }

        return returnType;
    }
}
