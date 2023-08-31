using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySlot : MonoBehaviour, IPointerDownHandler
{
    public InventoryManager inventoryManager;
    [HideInInspector] public DraggableItem draggableItem;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount == 0) {
            Transform item = inventoryManager.selectedItem;
            if (item)
            {
                draggableItem = item.GetComponent<DraggableItem>();
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
