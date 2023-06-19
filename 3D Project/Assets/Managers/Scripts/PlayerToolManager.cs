using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerToolManager", menuName = "Managers/PlayerToolManager")]
public class PlayerToolManager : ScriptableObject
{
    [Header("Tools")]
    public List<Tool> tools = new List<Tool>();

    [Header("HotBar Inputs")]
    public List<InputAction> hotbarKeys;

    private Transform player;
    private PlayerToolInteraction toolInteraction;

    public void Hotbar1(InputAction.CallbackContext context)
    {
        Debug.Log("Hotbar1");
        if (toolInteraction == null) Debug.Log("toolInteraction == null");
        if (tools[0] == null) Debug.Log("tools[0] == null");
        toolInteraction.SetTool(tools[0]);
    }

    public void Hotbar2(InputAction.CallbackContext context)
    {
        toolInteraction.SetTool(tools[1]);
    }

    private void OnEnable()
    {
        foreach (InputAction action in hotbarKeys)
            action.Enable();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        toolInteraction = player.GetComponent<PlayerToolInteraction>();

        hotbarKeys[0].performed += ctx => { Hotbar1(ctx); };
        hotbarKeys[1].performed += ctx => { Hotbar2(ctx); };
    }

    private void OnDisable()
    {
        foreach (InputAction action in hotbarKeys)
            action.Disable();
    }
}
