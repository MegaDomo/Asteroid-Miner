using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Interaction))]
[RequireComponent(typeof(ChestSlotSetup))]
public class Chest : MonoBehaviour
{
    [Header("Contents")]
    public InventoryObject inventory;

    [Header("UI References")]
    public Transform parentOfChestInventory;

    InventoryToggle inventoryToggle;
    Interaction interaction;
    ChestSlotSetup setup;

    private void InteractToToggle()
    {
        inventoryToggle.ToggleInventory(inventory);
    }

    private void Awake()
    {
        interaction = GetComponent<Interaction>();
        setup = GetComponent<ChestSlotSetup>();
        inventoryToggle = parentOfChestInventory.GetComponent<InventoryToggle>();

        setup.Setup(parentOfChestInventory, inventory);
    }

    private void OnEnable()
    {
        interaction.interactAction += InteractToToggle;
    }

    private void OnDisable()
    {
        interaction.interactAction -= InteractToToggle;
    }

    private void OnApplicationQuit()
    {
        // TODO : Save
        inventory.inventory.Clear();
        Array.Clear(inventory.items, 0, inventory.items.Length);
        inventory.items = null;
    }
}
