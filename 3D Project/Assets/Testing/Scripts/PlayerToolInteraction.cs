using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToolInteraction : MonoBehaviour
{
    [Header("Unity References")]
    public Transform playerCam;
    public Transform toolPoint;

    [Header("Attributes")]
    public float interactRange;
    public InputAction interactAction;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

    private Tool selectedTool;
    private GameObject toolObj;

    private bool drillingFinished;
    private float drillingTime;

    private void Update()
    {
        if (interactAction.IsPressed())
            CheckForToolUse();
    }

    private void CheckForToolUse()
    {
        // Switch Case (Polymorphism) to check which tool method to use
        if (selectedTool != null && selectedTool.toolType == Tool.ToolType.Drill)
            Drill();
    }

    public void Drill()
    {
        
        if (Physics.Raycast(playerCam.position, playerCam.forward, out interactHit,
            interactRange, LayerMask.GetMask("Drillable")))
        {
            inRangeForInteractable = true;
            drillingTime += Time.deltaTime;
            Debug.Log("Drilling");
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

    public void SetTool(Tool tool)
    {
        if (toolObj != null)
            Destroy(toolObj);

        if (tool == null)
            return;

        selectedTool = tool;
        toolObj = Instantiate(selectedTool.prefab, toolPoint.position, Quaternion.identity);
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
