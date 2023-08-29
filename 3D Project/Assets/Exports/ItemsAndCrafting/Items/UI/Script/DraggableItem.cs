using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI References")]
    public Image image;
    public TextMeshProUGUI text;

    // Util Vars
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public InventoryItem invItem;

    public void Setup(ItemObject item, int amount)
    {
        invItem = new InventoryItem(item, amount);
        image.sprite = item.sprite;
        UpdateTextAmount();
    }

    public void AddToExistingItem(int amount)
    {
        if (invItem == null)
            return;

        invItem.AddToAmount(amount);
        UpdateTextAmount();
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
}
