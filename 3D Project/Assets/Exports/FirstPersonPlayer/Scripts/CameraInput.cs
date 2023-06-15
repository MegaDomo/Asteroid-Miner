using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput : MonoBehaviour
{
    [Header("Scriptable Object")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public Transform playerBody;

    [Header("Attributes")]
    public float cameraSpeed = 1f;

    public InputAction cameraAction;

    float verticalRotation = 0;
    float horizontalRotation = 0;

    private bool isPlayerInControl = true;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() => MoveCamera();

    private void MoveCamera()
    {
        if (!playerManager.IsPlayerInControl())
            return;

        float inputX = Input.GetAxis("Mouse X") * cameraSpeed;
        float inputY = Input.GetAxis("Mouse Y") * cameraSpeed;

        horizontalRotation += inputX;
        verticalRotation -= inputY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Vector3 horizontalVec = Vector3.up * horizontalRotation;
        Vector3 verticalVec = Vector3.right * verticalRotation;

        transform.rotation = Quaternion.Euler(horizontalVec + verticalVec);

        playerBody.rotation = Quaternion.Euler(Vector3.up * transform.eulerAngles.y);
    }

    #region Utility
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
        cameraAction.Enable();
    }

    private void OnDisable()
    {
        cameraAction.Disable();
    }
    #endregion
}
