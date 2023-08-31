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
    [HideInInspector] public InventoryItem invItem;

    bool selectedItem = false;

    public void Setup(ItemObject item, int amount)
    {
        invItem = new InventoryItem(item, amount);
        image.sprite = item.sprite;
        UpdateTextAmount();
    }

    // Returns any Overflow
    public int AddToExistingItem(int amount)
    {
        if (invItem == null)
            return 0;

        
        if (invItem.amount + amount > invItem.maxStack)
        {
            int overflow = invItem.maxStack - (invItem.amount + amount);
            invItem.AddToAmount(overflow);
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
                inventoryManager.selectedItem = this;
                
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

    public void PlaceItem()
    {
        // Place all of Item
        selectedItem = false;
        inventoryManager.selectedItem = null;

        transform.SetParent(parentAfterDrag);
        Debug.Log(parentAfterDrag);
        GetComponent<Image>().raycastTarget = true;
    }
}
