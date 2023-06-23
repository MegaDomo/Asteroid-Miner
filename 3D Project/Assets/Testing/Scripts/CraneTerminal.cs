using UnityEngine;
using UnityEngine.InputSystem;

public class CraneTerminal : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Interaction interaction;
    public CharacterController controller;
    public Camera craneCam;
    private Camera playerCamera;

    [Header("Attributes")]
    public float craneSpeed = 10f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction exitAction;

    private bool isOperatingCrane;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerCamera = playerManager.GetPlayerCamera();

        craneCam.enabled = false;
    }

    private void Update()
    {
        MoveCrane();
    }

    private void MoveCrane()
    {
        if (!isOperatingCrane)
            return;

        float x = moveAction.ReadValue<Vector2>().x;
        float y = moveAction.ReadValue<Vector2>().y;
        Vector3 dir = new Vector3(x, 0, y).normalized;
        if (dir.magnitude >= 0.05f)
            controller.Move(dir * craneSpeed * Time.deltaTime);
    }

    public void InteractWithTerminal()
    {
        isOperatingCrane = true;
        playerManager.SetPlayerHasControl(false);
        SwapCameras();
    }

    public void StopInteractingWithTerminal(InputAction.CallbackContext context)
    {
        if (!isOperatingCrane)
            return;
        isOperatingCrane = false;
        playerManager.SetPlayerHasControl(true);
        SwapCameras();
    }

    private void SwapCameras()
    {
        craneCam.enabled = !craneCam.enabled;
        playerCamera.enabled = !playerCamera.enabled;
    }

    private void OnEnable()
    {
        interaction.interactAction += InteractWithTerminal;

        moveAction = playerInput.Crane.MoveAction;
        moveAction.Enable();

        exitAction = playerInput.Crane.ExitInteractable;
        exitAction.Enable();
        exitAction.performed += StopInteractingWithTerminal;
    }

    private void OnDisable()
    {
        interaction.interactAction -= InteractWithTerminal;

        moveAction.Disable();
        exitAction.Disable();
    }
}
