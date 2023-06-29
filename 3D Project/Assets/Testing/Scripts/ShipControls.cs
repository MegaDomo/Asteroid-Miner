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
    public float shipRotationSpeed = 1f;
    public GameObject[] shipTools;

    [Header("Rotation Correction")]
    public float xRotationCorrection;
    public float yRotationCorrection;
    public float zRotationCorrection;

    private bool inShip;
    private bool isFreeLooking = false;
    private float currentVelocity;
    private Transform player;
    private GameObject currentShipTool;

    private PlayerInput playerInput;
    private InputAction exitShipAction;
    private InputAction moveAction;
    private InputAction freeLookAction;
    private InputAction toggleToolAction;

    private void Awake()
    {
        playerInput = new PlayerInput();

        SetAudioListener(false);

        shipCam.GetComponent<Camera>().enabled = false;

        if (shipTools.Length != 0 && shipTools[0] != null)
            currentShipTool = shipTools[0];
    }

    private void FixedUpdate()
    {
        MoveShip();
        RotateShip();
    }

    private void MoveShip()
    {
        if (!inShip)
            return;

        float x = moveAction.ReadValue<Vector3>().x;
        float y = moveAction.ReadValue<Vector3>().y;
        float z = moveAction.ReadValue<Vector3>().z;
        Vector3 direction = new Vector3(x, 0, z);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            
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
        if (!inShip)
            return;

        if (isFreeLooking)
            return;

        Vector3 dir = shipCam.forward;
        Vector3 lookDirection = new Vector3(dir.x, 0f, dir.z);

        Quaternion endRoation = Quaternion.LookRotation(lookDirection, Vector3.up);

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, shipCam.eulerAngles.y, 
                                            ref currentVelocity, shipRotationSpeed);

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * 
                             Quaternion.Euler(xRotationCorrection, yRotationCorrection, zRotationCorrection);
    }

    public void EnterShip()
    {
        inShip = true;

        playerManager.SetPlayerHasControl(false);
        playerManager.SetInShip(true);

        player = playerManager.GetPlayer();
        player.gameObject.SetActive(false);

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
        playerManager.SetInShip(false);

        player = null;

        SetAudioListener(false);
        SwapCameras();
    }

    public void FreeLook(InputAction.CallbackContext context)
    {
        if (playerManager.IsPlayerInControl())
            return;
        isFreeLooking = !isFreeLooking;
    }

    public void ToggleTool(InputAction.CallbackContext context)
    {
        if (currentShipTool == null)
            return;

        if (currentShipTool.activeSelf)
            currentShipTool.SetActive(false);
        else
            currentShipTool.SetActive(true);
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

        toggleToolAction = playerInput.Ship.ToggleTool;
        toggleToolAction.Enable();
        toggleToolAction.performed += ToggleTool;
    }

    private void OnDisable()
    {
        interaction.interactAction -= EnterShip;

        exitShipAction.Disable();
        moveAction.Disable();
        freeLookAction.Disable();
        toggleToolAction.Disable();
    }
}
