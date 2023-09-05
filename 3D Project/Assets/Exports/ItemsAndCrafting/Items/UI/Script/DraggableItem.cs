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

        Debug.Log(invItem.maxStack + ", " + (invItem.amount + amount));
        if (invItem.amount + amount > invItem.maxStack) {
            
            int overflow = (invItem.amount + amount) - invItem.maxStack;
            int amountToAdd = invItem.maxStack - invItem.amount;
            invItem.AddToAmount(amountToAdd);
            UpdateTextAmount();
            return overflow;
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
        if (!selectedItem) {
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
    }

    public void PlaceItem(DisplaySlot displaySlot)
    {
        // Place all of Item
        selectedItem = false;
        inventoryManager.SetSelectedItem(null);
        this.displaySlot = displaySlot;

        transform.SetParent(parentAfterDrag);
        GetComponent<Image>().raycastTarget = true;
    }





/*
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        GetComponent<Image>().raycastTarget = true;
    }
*/

}