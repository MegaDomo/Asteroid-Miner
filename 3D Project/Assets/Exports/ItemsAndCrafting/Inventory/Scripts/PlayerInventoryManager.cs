using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("Scriptable Object References")]
    public InventoryObject inventory;

    [Header("UI References")]
    public List<Transform> allDisplaySlots;

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
        inventory.Setup(allDisplaySlots);
    }

    private void OnApplicationQuit()
    {
        // TODO : Save
        inventory.inventory.Clear();
    }
}
