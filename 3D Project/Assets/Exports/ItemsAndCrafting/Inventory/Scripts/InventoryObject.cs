using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Managers/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Unity References")]
    public GameObject draggableItemPrefab;
    public List<DisplaySlot> inventory = new List<DisplaySlot>();

    [HideInInspector] public InventoryItem[] items;

    public void Initialize(List<Transform> slots)
    {
        // Gets all DisplaySlots from UI
        for (int i = 0; i < slots.Count; i++)
            inventory.Add(slots[i].GetComponent<DisplaySlot>());
        items = new InventoryItem[slots.Count];
    }



    #region Picking up an Item from World Space
    public bool AddItem(ItemObject item, int amount)
    {
        // Adds to Existing Item 
        for (int i = 0; i < inventory.Count; i++) {
            DisplaySlot slot = inventory[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && itemInSlot.invItem.item == item) {
                itemInSlot.AddToExistingItem(amount);
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

    #region Load Content
    public void LoadContent()
    {
        foreach (DisplaySlot slot in inventory) {
            Transform transform = slot.transform;
            if (transform.childCount != 0)
                Destroy(transform.GetChild(0).gameObject);
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