using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("Scriptable Object References")]
    
    public InventoryManager inventoryManager;

    [Header("UI References")]
    public InventoryToggle inventoryToggle;
    public List<Transform> allDisplaySlots;

    Inventory inventory;

    PlayerInput playerInput;
    InputAction inventoryInput;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, item.amount);
            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        inventoryManager.SetPlayerInventory(inventory);
        inventory.Initialize(allDisplaySlots, "PlayerInventory");
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        inventoryInput = playerInput.Player.Inventory;
        inventoryInput.Enable();
        inventoryInput.performed += inventoryToggle.ToggleInventory;
    }

    private void OnDisable()
    {
        inventoryInput.Disable();
    }

    private void OnApplicationQuit()
    {
        inventory.Reset();
    }
}
