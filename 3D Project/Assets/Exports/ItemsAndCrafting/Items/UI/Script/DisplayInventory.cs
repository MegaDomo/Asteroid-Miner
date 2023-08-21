using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    Dictionary<ItemObject, GameObject> itemsDisplayed = new Dictionary<ItemObject, GameObject>();
    private void Awake()
    {
        inventory.itemPickedUp += UpdateDisplayItem;
    }

    void Start()
    {
        StartDisplay();
    }

    private void StartDisplay()
    {
        foreach (var item in inventory.container.Keys)
        {
            UpdateDisplayItem(item, inventory.container[item]);
        }
    }

    public void UpdateDisplayItem(ItemObject item, int amount)
    {
        if (itemsDisplayed.ContainsKey(item))
            itemsDisplayed[item].GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[item].ToString("n0");
        else
            AddDisplayItem(item, amount);
    }

    public void AddDisplayItem(ItemObject item, int amount)
    {
        var obj = Instantiate(item.UIElement, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString("n0");
        itemsDisplayed.Add(item, obj);
    }
}
