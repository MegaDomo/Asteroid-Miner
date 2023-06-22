using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementMode { GravityMode, SpaceMode }

public class FirstPersonPlayerMovement : MonoBehaviour
{
    [Header("File References")]
    public InputActionMap inputActionMap;
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Transform cam;
    public CharacterController controller;
    public Rigidbody rb;
    public CapsuleCollider capCollider;

    [Header("Attributes")]
    public float walkSpeed = 5f;
    public float flySpeed = 5f;
    public float interactDetectRange = 5f;

    public InputAction moveAction;

    private bool isPlayerInControl = true;
    private MovementMode movementMode;

    private void Update()
    {
        if (movementMode == MovementMode.GravityMode)
            GravityMove();
        else
            SpaceMove();
    }

    private void GravityMove()
    {
        if (!playerManager.IsPlayerInControl())
            return;
        bool foundInput = false;
        float x = inputActionMap.FindAction("MoveAction", foundInput).ReadValue<Vector2>().x;
        float z = inputActionMap.FindAction("MoveAction", foundInput).ReadValue<Vector2>().x;
        //float x = moveAction.ReadValue<Vector3>().x;
        //float z = moveAction.ReadValue<Vector3>().z;
        Vector3 direction = new Vector3(x, 0, z);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 dir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            controller.Move(dir * walkSpeed * Time.deltaTime);
        }
    }

    private void SpaceMove()
    {
        if (!playerManager.IsPlayerInControl())
            return;

        float x = moveAction.ReadValue<Vector3>().x;
        float y = moveAction.ReadValue<Vector3>().y;
        float z = moveAction.ReadValue<Vector3>().z;
        Vector3 direction = new Vector3(x, 0, z);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            Vector3 dir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            rb.AddForce(dir * flySpeed, ForceMode.Force);
        }

        Vector3 upDirection = new Vector3(0, y, 0);
        if (upDirection.magnitude >= 0.1f)
        {
            rb.AddForce(upDirection * flySpeed, ForceMode.Force);
        }
    }


    #region Utility
    public void SetAsSpaceModeMovement()
    {
        controller.enabled = false;
        rb.isKinematic = false;
        capCollider.enabled = true;
        movementMode = MovementMode.SpaceMode;
    }

    public void SetAsGravityModeMovement()
    {
        rb.isKinematic = true;
        capCollider.enabled = false;
        controller.enabled = true;
        movementMode = MovementMode.GravityMode;
    }

    public bool IsPlayerInControl()
    {
        return isPlayerInControl;
    }

    public void SetIsPlayerInControl(bool value)
    {
        isPlayerInControl = value;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }
    #endregion
}
