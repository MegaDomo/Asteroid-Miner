using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IPointerDownHandler
{
    [Header("Scriptable Object References")]
    public InventoryManager inventoryManager;

    [Header("UI References")]
    public Image image;
    public TextMeshProUGUI text;

    // Util Vars
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public DisplaySlot displaySlot;
    [HideInInspector] public InventoryItem invItem;

    bool selectedItem = false;

    public void Setup(ItemObject item, int amount, DisplaySlot startingSlot)
    {
        invItem = new InventoryItem(item, amount);
        image.sprite = item.sprite;
        displaySlot = startingSlot;
        UpdateTextAmount();
    }

    // Returns any Overflow
    public int AddToExistingItem(int amount)
    {
        if (invItem == null) {
            Debug.Log("Null Item Was Added To: " + gameObject.name);
            return 0;
        }

        // Overflow
        if (invItem.amount + amount > invItem.maxStack) {
            int overflow = (invItem.amount + amount) - invItem.maxStack;
            int amountToAdd = invItem.maxStack - invItem.amount;
            invItem.AddToAmount(amountToAdd);
            UpdateTextAmount();
            return overflow;
        }

        // Adding all of this item, needs to be Destroyed
        if (invItem.amount + amount <= 0) {
            displaySlot.draggableItem = null;
            Destroy(gameObject);
            return 0;
        }


        invItem.AddToAmount(amount);
        UpdateTextAmount();
        return 0;
    }

    public void UpdateTextAmount()
    {
        text.text = invItem.amount.ToString();
    }

    private void Update()
    {
        if (selectedItem)
            transform.position = Input.mousePosition;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // Picking up an Item
        if (inventoryManager.selectedItem == null) {

            // Left Click - Pick up All of Item
            if (eventData.button == PointerEventData.InputButton.Left) {
                selectedItem = true;
                inventoryManager.SetSelectedItem(transform);
                displaySlot.draggableItem = null;
                
                parentAfterDrag = transform.parent;
                transform.SetParent(transform.root);
                transform.SetAsLastSibling();
                GetComponent<Image>().raycastTarget = false;
            }
            // Right Click - Pick up Half of Item
            if (eventData.button == PointerEventData.InputButton.Right) {
                // Will need to create new Draggable
            }

        }

        // Holding an Item
        else {
            DraggableItem heldItem = inventoryManager.GetSelectedDraggableItem();
            if (heldItem) Debug.Log("Good Here");
            // Left Click - Add to Existing Item
            if (eventData.button == PointerEventData.InputButton.Left) {

                // Add All of Held Item
                if (invItem.amount + heldItem.invItem.amount <= invItem.maxStack) {
                    AddToExistingItem(heldItem.invItem.amount);
                    inventoryManager.DestroySelectedItem();
                }
                // Add as much as possible with overflow still selected
                else {
                    int overflow = invItem.amount + heldItem.invItem.amount - invItem.maxStack;
                    int fill = invItem.maxStack - invItem.amount;
                    AddToExistingItem(fill);
                    heldItem.AddToExistingItem(overflow - heldItem.invItem.amount);
                }

            }
            // Right Click - Add 1 to Existing Item
            if (eventData.button == PointerEventData.InputButton.Right) {

                // 1 Item was Held and is stored
                if (heldItem.invItem.amount == 1 && invItem.amount != invItem.maxStack) {
                    AddToExistingItem(1);
                    inventoryManager.DestroySelectedItem();
                }
                // Add as much as possible with overflow still selected
                else if (invItem.amount != invItem.maxStack) {
                    AddToExistingItem(1);
                    heldItem.AddToExistingItem(-1);
                }

            }
        }
    }

    // Placing an Item on an empty Slot
    public void PlaceItem(DisplaySlot displaySlot)
    {
        // Place all of Item
        selectedItem = false;
        inventoryManager.SetSelectedItem(null);
        this.displaySlot = displaySlot;

        transform.SetParent(parentAfterDrag);
        GetComponent<Image>().raycastTarget = true;
    }
}
