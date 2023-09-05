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
        
        if (draggableItem.transform.parent.tag == "PlayerInventory")
        {
            playerInventory.RemoveItem(draggableItem);
            chestInventory.AddItem(item.item, item.amount);
            return;
        }
        else // Chest Inventory
        {
            chestInventory.RemoveItem(draggableItem);
            playerInventory.AddItem(item.item, item.amount);
            return;
        }
    }

    public void DestroySelectedItem()
    {
        Destroy(selectedItem.gameObject);
        selectedItem = null;
    }

    public void SetChestInventory(InventoryObject inventory)
    {
        chestInventory = inventory;
    }

    public Transform GetSelectedItem()
    {
        return selectedItem;
    }

    public DraggableItem GetSelectedDraggableItem()
    {
        if (!selectedItem)
            return null;
        return selectedItem.GetComponent<DraggableItem>();
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
