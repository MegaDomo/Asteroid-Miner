using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Unity References")]
    public Transform cam;
    public GameObject interactUI;

    [Header("Attributes")]
    public float interactDetectRange;

    [Header("Debugging")]
    public bool drawFacingLine;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

    private PlayerInput playerInput;
    private InputAction interactAction;

    private void Awake()
    {
        playerInput = new PlayerInput();

        DisplayInteractUI(false);
    }

    void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        if (drawFacingLine)
            Debug.DrawRay(cam.position, cam.forward * interactDetectRange, Color.red, 0.01f);

        if (Physics.Raycast(cam.position, cam.forward, out interactHit,
            interactDetectRange, LayerMask.GetMask("Interactable")))
        {
            interactUI.SetActive(true);
            inRangeForInteractable = true;
        }
        else
        {
            interactUI.SetActive(false);
            inRangeForInteractable = false;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (!inRangeForInteractable || interactHit.transform == null)
            return;
        interactHit.transform.GetComponent<Interaction>().Interact();
    }

    public void DisplayInteractUI(bool value)
    {
        if (interactUI.activeSelf == value)
            return;
        interactUI.SetActive(value);
    }

    private void OnEnable()
    {
        interactAction = playerInput.Player.Interact;
        interactAction.Enable();
        interactAction.performed += Interact;
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }
}
