using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventorySlot
{
    public const int MAX = 999;

    public Item data;
    public int invenSize;

    public InventorySlot(Item item)
    {
        data = item;
        addItem();
    }

    public void addItem()
    {
        if (invenSize < MAX)
            invenSize++;
    }

    public void removeItem()
    {
        //if (invenSize! <= 0)
            invenSize--;
    }
}
