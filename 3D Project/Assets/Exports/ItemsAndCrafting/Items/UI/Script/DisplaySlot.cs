using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySlot : MonoBehaviour, IPointerDownHandler
{
    public InventoryManager inventoryManager;
    [HideInInspector] public DraggableItem draggableItem;

    // This Slot Receives an Item
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) {
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            draggableItem = inventoryManager.selectedItem;
            if (draggableItem)
            {
                draggableItem.parentAfterDrag = transform;
                draggableItem.PlaceItem();
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
