using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    private List<GameObject> displaySlots;
/*
    private void Awake()
    {
        inventory.itemPickedUp += UpdateDisplayItem;
    }

    void Start()
    {
        StartDisplay();
    }

    private void StartDisplay()
    {
        // Initializes the Slots
        for (int i = 0; i < transform.childCount; i++)
        {
            InventorySlot tempSlot = new InventorySlot(null, 0);
            itemsDisplayed.Add(tempSlot, transform.GetChild(i).gameObject);
        }
        
        // Fills Slots with any Save Data (or Data from Editor)
        for (int i = 0; i < inventory.container.Count; i++)
            UpdateDisplayItem(inventory.container[i]);
    }

    public void UpdateDisplayItem(InventorySlot slot)
    {
        // Update Existing item - TODO check if run out of item (reach "0" for amount)
        if (itemsDisplayed.ContainsKey(slot))
            itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        // Add Item to Display
        else
            AddDisplayItem(slot);
    }

    public void AddDisplayItem(InventorySlot slot)
    {
        var obj = Instantiate(slot.item.UIElement, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        itemsDisplayed.Add(slot, obj);
    }

    private void UpdateSprite()
    {

    }*/
}
