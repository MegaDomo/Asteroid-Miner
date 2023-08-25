using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{
    [Header("Unity References")]
    public Transform inventoryParent;

    PlayerInput playerInput;
    InputAction inventoryInput;

    CanvasGroup canvasGroup;

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        // Show
        if (canvasGroup.alpha == 0)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        // Hide
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
            
    }

    private void Awake()
    {
        canvasGroup = inventoryParent.GetComponent<CanvasGroup>();
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        inventoryInput = playerInput.Player.Inventory;
        inventoryInput.Enable();
        inventoryInput.performed += ToggleInventory;
    }
}