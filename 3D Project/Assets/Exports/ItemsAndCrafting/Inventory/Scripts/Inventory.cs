using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject draggableItemPrefab;

    [Header("Default Contents")]
    public List<InventoryItem> itemsToSpawnWith;

    [HideInInspector] public List<InventoryItem> items;

    List<DisplaySlot> inventorySlots = new List<DisplaySlot>();

    public void Initialize(List<Transform> slots, string tag)
    {
        // Gets all DisplaySlots from UI
        for (int i = 0; i < slots.Count; i++) {
            inventorySlots.Add(slots[i].GetComponent<DisplaySlot>());
            slots[i].tag = tag;
        }

        items = new List<InventoryItem>(slots.Count);

        // Initializes
        for (int i = 0; i < items.Capacity; i++)
            items.Add(null);

        // Fills with Preset items
        for (int i = 0; i < itemsToSpawnWith.Count; i++)
            items[i] = itemsToSpawnWith[i];
    }

    #region Picking up an Item from World Space
    public bool AddItem(ItemObject item, int amount)
    {
        // Adds to Existing Item 
        for (int i = 0; i < inventorySlots.Count; i++) {
            DisplaySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            
            if (itemInSlot != null && itemInSlot.invItem.item == item && item.maxStackSize != itemInSlot.invItem.amount) {
                int overflow = itemInSlot.AddToExistingItem(amount);
                if (overflow > 0)
                    AddItem(itemInSlot.invItem.item, overflow);
                items[i] = itemInSlot.invItem;
                return true;
            }
        }

        // Find Empty Slot
        for (int i = 0; i < inventorySlots.Count; i++) {
            DisplaySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null) {
                SpawnNewItem(i, item, amount, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(int index, ItemObject item, int amount, DisplaySlot slot)
    {
        GameObject newItem = Instantiate(draggableItemPrefab, slot.transform);
        DraggableItem draggableItem = newItem.GetComponent<DraggableItem>();
        draggableItem.Setup(item, amount, slot);
        slot.draggableItem = draggableItem;

        // Recording
        items[index] = draggableItem.invItem;
    }
    #endregion

    public void RemoveItem(DraggableItem item)
    {
        item.displaySlot.draggableItem = null;
        Destroy(item.gameObject);
    }

    #region Load Content
    public void LoadContent()
    {
        // Clears Display Slots
        foreach (DisplaySlot slot in inventorySlots) {
            Transform transform = slot.transform;
            if (transform.childCount != 0)
                Destroy(transform.GetChild(0).gameObject);
        }

        // Fills DisplaySlots with stored Items from this inventory
        int index = 0;
        foreach (DisplaySlot slot in inventorySlots) {
            if (items[index] != null) {
                GameObject newItem = Instantiate(draggableItemPrefab, slot.transform);
                DraggableItem draggableItem = newItem.GetComponent<DraggableItem>();
                draggableItem.Setup(items[index].item, items[index].amount, slot);
                slot.draggableItem = draggableItem;
            }
            index++;
        }
    }

    public void SaveContent()
    {
        int index = 0;
        foreach (DisplaySlot slot in inventorySlots) {
            if (slot.draggableItem)
            {
                if (slot.draggableItem == null) Debug.Log("Drag");
                if (slot.draggableItem.invItem == null) Debug.Log(".invItem");
                items[index] = slot.draggableItem.invItem;
            }
            else
                items[index] = null;
            index++;
        }
    }
    #endregion

    public void Reset()
    {
        // TODO : Save
        inventorySlots?.Clear();
        items?.Clear();
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;
    [HideInInspector] public int maxStack;
    public InventoryItem(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
        if (item == null) Debug.Log("Yep");
        maxStack = item.maxStackSize;
    }

    public void AddToAmount(int amount)
    {
        this.amount += amount;
    }
}