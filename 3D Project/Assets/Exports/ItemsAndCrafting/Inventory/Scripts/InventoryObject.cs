using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Managers/Inventory")]
public class InventoryObject : ScriptableObject
{
    public Dictionary<ItemObject, int> container = new Dictionary<ItemObject, int>();

    public Action<ItemObject, int> itemPickedUp;

    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container.ContainsKey(item))
            {
                container[item] += amount;
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            container.Add(item, amount);
        }

        itemPickedUp.Invoke(item, amount);
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddItem(int amount)
    {
        this.amount += amount;
    }
}