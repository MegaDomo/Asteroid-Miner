using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class RaycastShiftClick : MonoBehaviour
{
    [Header("Scriptable Object References")]
    public InventoryManager inventoryManager;

    PlayerInput playerInput;
    InputAction doubleClick;

    void ShiftClick(InputAction.CallbackContext context)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(data, results);

        foreach (var hit in results) {
            if (hit.gameObject.tag == "UIItem") {
                DraggableItem item = hit.gameObject.GetComponent<DraggableItem>();
                inventoryManager.TransferItemsBetweenInventories(item);
            }
        }
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        doubleClick = playerInput.Player.TransferItem;
        doubleClick.Enable();
        doubleClick.performed += ShiftClick;
    }

    private void OnDisable()
    {
        doubleClick.Disable();
    }
}
