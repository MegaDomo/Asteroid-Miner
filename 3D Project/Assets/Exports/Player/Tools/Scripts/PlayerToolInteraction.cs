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
    

    [Header("Debugging")]
    public Tool selectedTool;
    public float force;
    public float forceOffset = 0.1f;

    private bool inRangeForInteractable;
    private RaycastHit interactHit;

    private GameObject toolObj;

    private PlayerInput playerInput;
    private InputAction interactAction;
    private InputAction toolToggle;

    private bool drillingFinished;
    private float drillingTime;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        //if (interactAction.IsPressed())
        //    Drill();

        if (Input.GetMouseButtonDown(0))
            Drill();
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
        if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit,
            interactRange, LayerMask.GetMask("ToolInteractable")))
        {
            MeshDeformer1 deformer = hit.collider.GetComponent<MeshDeformer1>();
            Vector3 point = hit.point;
            point += hit.normal * forceOffset;
            if (deformer)
                deformer.AddDeformingForce(point, force);
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
        toolObj.transform.SetParent(transform);
    }

    public void ToolToggle(InputAction.CallbackContext context)
    {
        if (selectedTool == null)
            return;

        selectedTool.ToolInteraction(toolObj);
    }

    private void OnEnable()
    {
        interactAction = playerInput.Player.ToolHoldAction;
        interactAction.Enable();

        toolToggle = playerInput.Player.ToolToggle;
        toolToggle.Enable();
        toolToggle.performed += ToolToggle;
    }

    private void OnDisable()
    {
        interactAction.Disable();
        toolToggle.Disable();
    }
}
