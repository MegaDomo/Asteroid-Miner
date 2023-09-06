using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySlot : MonoBehaviour, IPointerDownHandler
{
    [Header("Scriptable Object References")]
    public InventoryManager inventoryManager;
    [HideInInspector] public DraggableItem draggableItem;

    [Header("Unity References")]
    public GameObject draggableItemPrefab;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        DraggableItem selectedItem = inventoryManager.GetSelectedDraggableItem();
        if (!selectedItem && transform.childCount == 0)
            return;

        // Left Click - Add all in Empty Slot
        if (eventData.button == PointerEventData.InputButton.Left) {
            draggableItem = selectedItem.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            draggableItem.PlaceItem(this);
        }

        // Right Click - Add 1 to Empty Slot
        if (eventData.button == PointerEventData.InputButton.Right) {

            // 1 Item was Held and is stored
            if (selectedItem.invItem.amount == 1) {
                draggableItem = selectedItem.GetComponent<DraggableItem>();
                draggableItem.parentAfterDrag = transform;
                draggableItem.PlaceItem(this);
            }
            // Stores 1 from a stack
            else {
                CreateNewDraggableItem(selectedItem);
                selectedItem.AddToExistingItem(-1);
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

    private void CreateNewDraggableItem(DraggableItem selectedItem)
    {
        draggableItem = Instantiate(draggableItemPrefab, transform).GetComponent<DraggableItem>();
        draggableItem.GetComponent<RectTransform>().sizeDelta = new Vector2(draggableItem.UIWidth, draggableItem.UIHeight);
        draggableItem.parentAfterDrag = transform;
        draggableItem.PlaceItem(this);
        draggableItem.Setup(selectedItem.invItem.item, 1, this);
    }
}
