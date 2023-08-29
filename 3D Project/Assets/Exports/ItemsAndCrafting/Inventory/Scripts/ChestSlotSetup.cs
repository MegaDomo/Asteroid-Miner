using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlotSetup : MonoBehaviour
{
    [Header("Contents")]
    public InventoryObject inventory;

    [Header("UI Reference")]
    public Transform parentOfChestInventorySlots;

    List<Transform> allDisplaySlots;

    private void Start()
    {
        for (int i = 0; i < parentOfChestInventorySlots.childCount; i++)
            allDisplaySlots.Add(parentOfChestInventorySlots.GetChild(i));

        inventory.Setup(allDisplaySlots);
    }
}
