using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [Header("Manager")]
    public InventoryManager inventoryManager;

    [Header("Inventory to Toggle")]
    public CanvasGroup canvasGroup;

    Inventory currentInventory;

    // For Player
    public void ToggleInventory(InputAction.CallbackContext context)
    {
        // Show
        if (canvasGroup.alpha == 0) {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        // Hide
        else {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
    }

    public void ToggleInventory(Inventory inventory)
    {
        // Show
        if (canvasGroup.alpha == 0) {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            inventory.LoadContent();
            currentInventory = inventory;
            inventoryManager.SetChestInventory(currentInventory);
        }
        // Hide
        else {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;

            inventory.SaveContent(); // TODO : Better System
            currentInventory = null;
            inventoryManager.SetChestInventory(currentInventory);
        }
    }
}
