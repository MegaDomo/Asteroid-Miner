using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToolManager : MonoBehaviour
{
    [Header("Tools")]
    public List<Tool> tools = new List<Tool>();

    [Header("HotBar Inputs")]
    public List<InputAction> hotbarKeys;

    private Transform player;
    private PlayerToolInteraction toolInteraction;

    private void Awake()
    {
        toolInteraction = GetComponent<PlayerToolInteraction>();

        hotbarKeys[0].performed += ctx => { Hotbar1(ctx); };
        hotbarKeys[1].performed += ctx => { Hotbar2(ctx); };
        hotbarKeys[2].performed += ctx => { Hotbar3(ctx); };
        hotbarKeys[3].performed += ctx => { Hotbar4(ctx); };
    }

    public void Hotbar1(InputAction.CallbackContext context)
    {
        toolInteraction.SetTool(tools[0]);
    }

    public void Hotbar2(InputAction.CallbackContext context)
    {
        toolInteraction.SetTool(tools[1]);
    }

    public void Hotbar3(InputAction.CallbackContext context)
    {
        toolInteraction.SetTool(tools[2]);
    }

    public void Hotbar4(InputAction.CallbackContext context)
    {
        toolInteraction.SetTool(tools[3]);
    }

    private void OnEnable()
    {
        foreach (InputAction action in hotbarKeys)
            action.Enable();
    }

    private void OnDisable()
    {
        foreach (InputAction action in hotbarKeys)
            action.Disable();
    }
}
