using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [Header("Inventory to Toggle")]
    public CanvasGroup canvasGroup;

    InventoryObject currentInventory;

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

    public void ToggleInventory(InventoryObject inventoryObject)
    {
        // Show
        if (canvasGroup.alpha == 0) {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            inventoryObject.LoadContent();
        }
        // Hide
        else {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            inventoryObject.SaveContent(); // TODO : Better System
        }
    }
}
