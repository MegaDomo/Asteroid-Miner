using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToolInteraction : MonoBehaviour
{
    [Header("Unity References")]
    public Transform playerCam;

    [Header("Attributes")]
    public float interactRange;
    public InputAction interactAction;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

    private bool drillingFinished;
    private float drillingTime;

    private void Update()
    {
        if (interactAction.IsPressed())
            CheckForToolUse();
    }

    private void CheckForToolUse()
    {
        Debug.DrawRay(playerCam.position, playerCam.forward * interactRange, Color.blue, 0.01f);

        if (Physics.Raycast(playerCam.position, playerCam.forward, out interactHit,
            interactRange, LayerMask.GetMask("Drillable")))
        {
            inRangeForInteractable = true;
            drillingTime += Time.deltaTime;

            DrillableSurface surface = interactHit.transform.GetComponent<DrillableSurface>();
            float timeToDrill = surface.timeToDrill;
            if (drillingTime >= timeToDrill)
                surface.CompleteDrilling();
        }
        else
        {
            inRangeForInteractable = false;
            drillingTime = 0f;
        }
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
