using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySlot : MonoBehaviour, IPointerDownHandler
{
    [Header("Scriptable Object References")]
    public InventoryManager inventoryManager;
    [HideInInspector] public DraggableItem draggableItem;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount == 0) {
            Transform item = inventoryManager.selectedItem;
            if (item) {
                draggableItem = item.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;
                draggableItem.PlaceItem(this);
            }
        }

        DraggableItem heldItem = inventoryManager.GetSelectedDraggableItem();
        // Right Click - Add 1 to Existing Item
        if (heldItem && eventData.button == PointerEventData.InputButton.Right) {

            // 1 Item was Held and is stored
            if (heldItem.invItem.amount == 1 && draggableItem.invItem.amount != draggableItem.invItem.maxStack) {
                draggableItem.AddToExistingItem(1);
                inventoryManager.DestroySelectedItem();
            }
            // Add as much as possible with overflow still selected
            else if (draggableItem.invItem.amount != draggableItem.invItem.maxStack) {
                draggableItem.AddToExistingItem(1);
                heldItem.AddToExistingItem(-1);
            }

        }
    }

    public void Clear()
    {
        if (!draggableItem)
            return;
        Destroy(draggableItem.gameObject);
        draggableItem = null;
    }
}
