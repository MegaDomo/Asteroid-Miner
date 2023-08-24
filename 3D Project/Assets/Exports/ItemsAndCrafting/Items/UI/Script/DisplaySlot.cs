using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySlot : MonoBehaviour, IDropHandler
{
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
}
