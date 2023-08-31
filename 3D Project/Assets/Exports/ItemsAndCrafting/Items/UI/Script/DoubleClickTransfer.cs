using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DoubleClickTransfer : MonoBehaviour//, IPointerClickHandler
{
    [Header("Scriptable Object References")]
    public InventoryManager inventoryManager;

    PlayerInput playerInput;
    InputAction doubleClick;

    private void ShiftClickTrigger(InputAction.CallbackContext context)
    {
        DraggableItem item = GetComponent<DraggableItem>();
        inventoryManager.TransferItemsBetweenInventories(item);
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        doubleClick = playerInput.Player.TransferItem;
        doubleClick.Enable();
        doubleClick.performed += ShiftClickTrigger;
    }

    private void OnDisable()
    {
        doubleClick.Disable();
    }

    /*
        int numberOfClicks = 0;

        float countdown = 0;
        float threshold = 0.4f;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (numberOfClicks == 1)
            {
                DoubleClickTrigger();
                numberOfClicks = 0;
            }
            if (numberOfClicks == 0)
                numberOfClicks = 1;
        }

        void Update()
        {
            if (numberOfClicks == 1)
                countdown += Time.deltaTime;

            if (countdown > threshold)
            {
                countdown = 0f;
                numberOfClicks = 0;
            }

        }
    */


}
