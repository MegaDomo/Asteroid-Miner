using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Unity References")]
    public Transform cam;
    public GameObject interactUI;

    [Header("Attributes")]
    public float interactDetectRange;

    public InputAction interactAction;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

    private void Awake()
    {
        interactAction.performed += ctx => { Interact(ctx); };
        DisplayInteractUI(false);
    }

    void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Debug.DrawRay(cam.position, cam.forward * interactDetectRange, Color.red, 0.01f);

        if (Physics.Raycast(cam.position, cam.forward, out interactHit,
            interactDetectRange, LayerMask.GetMask("Interactable")))
        {
            inRangeForInteractable = true;
        }
        else
        {
            inRangeForInteractable = true;
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
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }
}
