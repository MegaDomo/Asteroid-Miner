using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Managers/Inventory")]
public class InventoryObject : ScriptableObject
{
    //public Dictionary<ItemObject, int> container = new Dictionary<ItemObject, int>();
    public List<InventorySlot> container = new List<InventorySlot>();

    public Action<InventorySlot> itemPickedUp;

    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;
        InventorySlot slot;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                hasItem = true;
                container[i].amount += amount;
                slot = container[i];
                itemPickedUp.Invoke(slot);
                break;
            }
        }

        if (!hasItem)
        {
            slot = new InventorySlot(item, amount);
            container.Add(slot);
            itemPickedUp.Invoke(slot);
        }
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