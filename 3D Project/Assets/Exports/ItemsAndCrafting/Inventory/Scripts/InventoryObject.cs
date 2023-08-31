using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Managers/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Default Contents")]
    public List<InventoryItem> itemsToSpawnWith;

    [Header("Unity References")]
    public GameObject draggableItemPrefab;
    public List<DisplaySlot> inventory = new List<DisplaySlot>();

    [HideInInspector] public List<InventoryItem> items;

    public void Initialize(List<Transform> slots, string tag)
    {
        // Gets all DisplaySlots from UI
        for (int i = 0; i < slots.Count; i++) {
            inventory.Add(slots[i].GetComponent<DisplaySlot>());
            slots[i].tag = tag;
        }

        items = new List<InventoryItem>(slots.Count);

        for (int i = 0; i < items.Capacity; i++)
            items.Add(null);
    }

    #region Picking up an Item from World Space
    public bool AddItem(ItemObject item, int amount)
    {
        // Adds to Existing Item 
        for (int i = 0; i < inventory.Count; i++) {
            DisplaySlot slot = inventory[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && itemInSlot.invItem.item == item && item.maxStackSize >= itemInSlot.invItem.amount + amount) {
                int overflow = itemInSlot.AddToExistingItem(amount);
                if (overflow > 0)
                    AddItem(itemInSlot.invItem.item, overflow);
                items[i] = itemInSlot.invItem;
                return true;
            }
        }

        // Find Empty Slot
        for (int i = 0; i < inventory.Count; i++) {
            DisplaySlot slot = inventory[i];
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
        draggableItem.Setup(item, amount);

        // Recording
        items[index] = draggableItem.invItem;
    }
    #endregion

    public void RemoveItem(DraggableItem item)
    {
        Destroy(item.gameObject);
    }

    #region Load Content
    public void LoadContent()
    {
        // Clears Display Slots
        foreach (DisplaySlot slot in inventory) {
            Transform transform = slot.transform;
            if (transform.childCount != 0)
                Destroy(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null) Debug.Log("Null");
            if (items[i] != null) Debug.Log(items[i]);
        }

        // Fills DisplaySlots with stored Items from this inventory
        int index = 0;
        foreach (DisplaySlot slot in inventory) {
            if (items[index] != null) {
                GameObject newItem = Instantiate(draggableItemPrefab, slot.transform);
                DraggableItem draggableItem = newItem.GetComponent<DraggableItem>();
                draggableItem.Setup(items[index].item, items[index].amount);
                slot.draggableItem = draggableItem;
            }
            index++;
        }
    }

    public void SaveContent()
    {
        int index = 0;
        foreach (DisplaySlot slot in inventory) {
            if (slot.transform.childCount != 0)
                items[index] = slot.draggableItem.invItem;
            else
                items[index] = null;
            index++;
        }
    }
    #endregion

    private void OnDisable()
    {
        // TODO : Save
        inventory?.Clear();
        items?.Clear();
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;
    public int maxStack;
    public InventoryItem(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
        maxStack = item.maxStackSize;
    }

    public void AddToAmount(int amount)
    {
        this.amount += amount;
    }
}