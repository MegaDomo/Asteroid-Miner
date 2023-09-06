using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Interaction))]
public class Chest : MonoBehaviour
{
    [Header("UI References")]
    public Transform parentOfChestInventory;

    Inventory inventory;
    Interaction interaction;
    InventoryToggle inventoryToggle;

    List<Transform> allDisplaySlots = new List<Transform>();

    private void InteractToToggle()
    {
        inventoryToggle.ToggleInventory(inventory);
    }

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        interaction = GetComponent<Interaction>();
        inventoryToggle = parentOfChestInventory.GetComponent<InventoryToggle>();

        Setup();
    }

    public void Setup()
    {
        for (int i = 0; i < parentOfChestInventory.childCount; i++)
            allDisplaySlots.Add(parentOfChestInventory.GetChild(i));

        inventory.Initialize(allDisplaySlots, "ChestInventory");
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
        inventory.Reset();
    }
}
