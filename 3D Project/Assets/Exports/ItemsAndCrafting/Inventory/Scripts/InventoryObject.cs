using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Managers/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Unity References")]
    public GameObject draggableItemPrefab;
    public List<DisplaySlot> inventory = new List<DisplaySlot>();

    public Action<InventoryItem> itemPickedUp;

    public void Setup(List<Transform> slots)
    {
        // Gets all DisplaySlots from UI
        for (int i = 0; i < slots.Count; i++)
            inventory.Add(slots[i].GetComponent<DisplaySlot>());
    }

    public bool AddItem(ItemObject item, int amount)
    {
        // Adds to Existing Item 
        for (int i = 0; i < inventory.Count; i++) {
            DisplaySlot slot = inventory[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && itemInSlot.slot.item == item) {
                itemInSlot.AddToExistingItem(amount);
                return true;
            }
        }

        // Find Empty Slot
        for (int i = 0; i < inventory.Count; i++) {
            DisplaySlot slot = inventory[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, amount, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(ItemObject item, int amount, DisplaySlot slot)
    {
        GameObject newItem = Instantiate(draggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItem.GetComponent<DraggableItem>();
        draggableItem.Setup(item, amount);
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;
    public InventoryItem(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddToAmount(int amount)
    {
        this.amount += amount;
    }
}