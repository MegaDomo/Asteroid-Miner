using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity Refernces")]
    public Transform playerCam;
    public Transform shipCam;
    public Rigidbody rb;
    public AudioListener audioListener;
    public Interaction interaction;
    public VehicleDropOff vehicleDropOff;

    [Header("Attributes")]
    public float shipSpeed = 10f;
    public float cameraSpeed = 1f;
    public float balanceCorrectionSpeed = 10f;

    

    private bool inShip;
    private bool isFreeLooking = false;

    private PlayerInput playerInput;
    private InputAction exitShipAction;
    private InputAction moveAction;
    private InputAction freeLookAction;

    private void Awake()
    {
        playerInput = new PlayerInput();

        SetAudioListener(false);

        shipCam.GetComponent<Camera>().enabled = false;
    }

    private void FixedUpdate()
    {
        MoveShip();
        RotateShip();
    }

    private void MoveShip()
    {
        if (playerManager.IsPlayerInControl())
            return;

        float x = moveAction.ReadValue<Vector3>().x;
        float y = moveAction.ReadValue<Vector3>().y;
        float z = moveAction.ReadValue<Vector3>().z;
        Vector3 direction = new Vector3(x, 0, z);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            /*if (!isFreeLooking)
                targetAngle += shipCam.transform.eulerAngles.y;
            else
                targetAngle += transform.eulerAngles.y;
*/
            targetAngle += transform.eulerAngles.y;
            Vector3 dir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            rb.AddForce(dir * shipSpeed, ForceMode.Force);
        }

        Vector3 upDirection = new Vector3(0, y, 0);
        if (upDirection.magnitude >= 0.1f)
        {
            rb.AddForce(upDirection * shipSpeed, ForceMode.Force);
        }
    }

    private void RotateShip()
    {
        if (playerManager.IsPlayerInControl())
            return;

        if (isFreeLooking)
            return;

        Vector3 dir = shipCam.forward;
        Vector3 lookDirection = new Vector3(dir.x, 0f, dir.z);
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }

    public void EnterShip()
    {
        inShip = true;

        playerManager.SetPlayerHasControl(false);
        playerManager.GetPlayer().gameObject.SetActive(false);

        SetAudioListener(true);
        SwapCameras();
    }

    public void ExitShip(InputAction.CallbackContext context)
    {
        if (!inShip)
            return;
        inShip = false;
        vehicleDropOff.DropOffPlayer();

        playerManager.SetPlayerHasControl(true);

        SetAudioListener(false);
        SwapCameras();
    }

    public void FreeLook(InputAction.CallbackContext context)
    {
        if (playerManager.IsPlayerInControl())
            return;
        isFreeLooking = !isFreeLooking;
    }

    private void SwapCameras()
    {
        playerCam.GetComponent<Camera>().enabled = !playerCam.GetComponent<Camera>().enabled;
        shipCam.GetComponent<Camera>().enabled = !shipCam.GetComponent<Camera>().enabled;
    }

    public void SetAudioListener(bool value)
    {
        audioListener.enabled = value;
    }

    private void OnEnable()
    {
        interaction.interactAction += EnterShip;

        moveAction = playerInput.Ship.MoveAction;
        moveAction.Enable();

        exitShipAction = playerInput.Ship.ExitInteractable;
        exitShipAction.Enable();
        exitShipAction.performed += ExitShip;

        freeLookAction = playerInput.Ship.FreeLook;
        freeLookAction.Enable();
        freeLookAction.performed += FreeLook;
    }

    private void OnDisable()
    {
        interaction.interactAction -= EnterShip;

        exitShipAction.Disable();
        moveAction.Disable();
        freeLookAction.Disable();
    }
}
