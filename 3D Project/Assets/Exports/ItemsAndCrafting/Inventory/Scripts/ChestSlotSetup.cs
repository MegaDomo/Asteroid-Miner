using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlotSetup : MonoBehaviour
{
    List<Transform> allDisplaySlots = new List<Transform>();

    public void Setup(Transform parentOfChestInventorySlots, InventoryObject inventory)
    {
        for (int i = 0; i < parentOfChestInventorySlots.childCount; i++)
            allDisplaySlots.Add(parentOfChestInventorySlots.GetChild(i));

        inventory.Initialize(allDisplaySlots, "ChestInventory");
    }
}
