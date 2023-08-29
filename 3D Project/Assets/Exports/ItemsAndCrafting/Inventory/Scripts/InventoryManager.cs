using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryManager", menuName = "Managers/Inventory Manager")]
public class InventoryManager : ScriptableObject
{
    [Header("Unity References")]
    public InventoryObject playerInventory;
    InventoryObject chestInventory;

    Transform parentOfPlayerInventory;
    Transform parentOfChestInventory;

    public void Intialize(Transform parentOfPlayerInventory, Transform parentOfChestInventory)
    {
        this.parentOfPlayerInventory = parentOfPlayerInventory;
        this.parentOfChestInventory = parentOfChestInventory;
    }

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

        // If we made it through the loop then it must be in the chest
        chestInventory.RemoveItem(draggableItem);
        playerInventory.AddItem(item.item, item.amount);
    }

    public void SetChestInventory(InventoryObject inventory)
    {
        chestInventory = inventory;
    }
}
