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

    [Header("Attributes")]
    public float craneSpeed = 10f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction exitAction;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        MoveCrane();
    }

    private void MoveCrane()
    {
        if (playerManager.IsPlayerInControl())
            return;

        float x = moveAction.ReadValue<Vector3>().x;
        float z = moveAction.ReadValue<Vector3>().z;
        Vector3 dir = new Vector3(x, 0, z).normalized;
        if (dir.magnitude >= 0.05f)
            controller.Move(dir * craneSpeed * Time.deltaTime);
    }

    public void InteractWithTerminal()
    {
        playerManager.SetPlayerHasControl(false);
    }

    public void StopInteractingWithTerminal(InputAction.CallbackContext context)
    {
        playerManager.SetPlayerHasControl(true);
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
