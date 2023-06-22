using UnityEngine;
using UnityEngine.InputSystem;

public class CraneTerminal : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Interaction interaction;
    public Camera craneCam;

    [Header("Attribute")]
    public InputAction exitAction;

    private void Awake()
    {
        exitAction.performed += ctx => { StopInteractingWithTerminal(ctx); };
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

        exitAction.Enable();
    }

    private void OnDisable()
    {
        interaction.interactAction -= InteractWithTerminal;

        exitAction.Disable();
    }
}
