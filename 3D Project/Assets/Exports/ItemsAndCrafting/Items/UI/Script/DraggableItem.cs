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

    [Header("Unity References")]
    public GameObject draggableItemPrefab;
    public int UIWidth = 70;
    public int UIHeight = 70;

    [Header("UI References")]
    public Image image;
    public TextMeshProUGUI text;

    // Util Vars
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public DisplaySlot displaySlot;
    [HideInInspector] public InventoryItem invItem;

    [HideInInspector] public bool selectedItem = false;

    public void Setup(ItemObject item, int amount, DisplaySlot startingSlot)
    {
        invItem = new InventoryItem(item, amount);
        image.sprite = item.sprite;
        displaySlot = startingSlot;
        UpdateTextAmount();
    }

    public void Setup(ItemObject item, int amount)
    {
        invItem = new InventoryItem(item, amount);
        image.sprite = item.sprite;
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
                if (invItem.amount == 1)
                    return;
                DraggableItem secondItem = Instantiate(draggableItemPrefab, displaySlot.transform).GetComponent<DraggableItem>();
                secondItem.GetComponent<RectTransform>().sizeDelta = new Vector2(UIWidth, UIHeight);

                secondItem.selectedItem = true;
                inventoryManager.SetSelectedItem(secondItem.transform);

                int halfCeil = Mathf.CeilToInt(invItem.amount / 2);
                secondItem.Setup(invItem.item, halfCeil, displaySlot);
                AddToExistingItem(-halfCeil);

                displaySlot.draggableItem = null;
                secondItem.parentAfterDrag = transform.parent;
                secondItem.transform.SetParent(transform.root);
                secondItem.transform.SetAsLastSibling();
                secondItem.GetComponent<Image>().raycastTarget = false;
            }

        }

        // Holding an Item
        else {
            DraggableItem selectedItem = inventoryManager.GetSelectedDraggableItem();
            
            // Left Click - Add to Existing Item
            if (eventData.button == PointerEventData.InputButton.Left) {

                // Add All of Held Item
                if (invItem.amount + selectedItem.invItem.amount <= invItem.maxStack) {
                    AddToExistingItem(selectedItem.invItem.amount);
                    inventoryManager.DestroySelectedItem();
                }
                // Add as much as possible with overflow still selected
                else {
                    int overflow = invItem.amount + selectedItem.invItem.amount - invItem.maxStack;
                    int fill = invItem.maxStack - invItem.amount;
                    AddToExistingItem(fill);
                    selectedItem.AddToExistingItem(overflow - selectedItem.invItem.amount);
                }

            }
            // Right Click - Add 1 to Existing Item
            if (eventData.button == PointerEventData.InputButton.Right) {

                // 1 Item was Held and is stored
                if (selectedItem.invItem.amount == 1 && invItem.amount != invItem.maxStack) {
                    AddToExistingItem(1);
                    inventoryManager.DestroySelectedItem();
                }
                // Add as much as possible with overflow still selected
                else if (invItem.amount != invItem.maxStack) {
                    AddToExistingItem(1);
                    selectedItem.AddToExistingItem(-1);
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
