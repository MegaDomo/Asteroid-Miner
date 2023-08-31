using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryManager", menuName = "Managers/Inventory Manager")]
public class InventoryManager : ScriptableObject
{
    [Header("Unity References")]
    public InventoryObject playerInventory;
    InventoryObject chestInventory;

    public Transform selectedItem;

    public void TransferItemsBetweenInventories(DraggableItem draggableItem)
    {
        if (chestInventory == null)
            return;

        InventoryItem item = draggableItem.invItem;
        Debug.Log("Yes");
        if (draggableItem.transform.parent.tag == "PlayerInventory")
        {
            Debug.Log("Fire");
            playerInventory.RemoveItem(draggableItem);
            chestInventory.AddItem(item.item, item.amount);
            return;
        }
        else // Chest Inventory
        {
            Debug.Log("Water");
            chestInventory.RemoveItem(draggableItem);
            playerInventory.AddItem(item.item, item.amount);
            return;
        }
    }

    public void SetChestInventory(InventoryObject inventory)
    {
        chestInventory = inventory;
    }

    public void SetSelectedItem(Transform selectedItem)
    {
        this.selectedItem = selectedItem;
    }

    private void OnDisable()
    {
        selectedItem = null;
        chestInventory = null;
    }
}
