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

    [Header("Debugging")]
    public Tool selectedTool;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

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
        if (selectedTool != null)
        {
            if (Physics.Raycast(playerCam.position, playerCam.forward, out interactHit,
            interactRange, LayerMask.GetMask("ToolInteractable")))
            {
                inRangeForInteractable = true;
                ToolInteraction toolInteraction = interactHit.transform.GetComponent<ToolInteraction>();

                if (selectedTool.toolType != toolInteraction.toolType)
                    return;

                float interactTime = toolInteraction.GetTimeToFinish();

                drillingTime += Time.deltaTime;
                if (drillingTime >= interactTime)
                    toolInteraction.ToolInteractionComplete();
            }
            else
            {
                inRangeForInteractable = false;
                drillingTime = 0f;
            }
        }
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
