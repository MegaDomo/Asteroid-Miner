using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IPointerDownHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler
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
            // Pick up All of Item
            if (eventData.button == PointerEventData.InputButton.Left) {
                // Update Method will handle the Transforming
                selectedItem = true;
                inventoryManager.SetSelectedItem(transform);
                displaySlot.draggableItem = null;
                
                parentAfterDrag = transform.parent;
                transform.SetParent(transform.root);
                transform.SetAsLastSibling();
                GetComponent<Image>().raycastTarget = false;
            }
            // Pick up Half of Item
            if (eventData.button == PointerEventData.InputButton.Right) {
                // Will need to create new Draggable
            }
        }
        // This item Placing an Item on Another Item
        else {
            // 2 Cases. - all of selected item is stored. 
            //          - some of selected item is stored and other part of item is still selected
            DraggableItem heldItem = inventoryManager.GetSelectedDraggableItem();
            // Add all of selected item into stored item
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
