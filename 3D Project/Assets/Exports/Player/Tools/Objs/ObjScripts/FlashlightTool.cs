using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlashlightTool", menuName = "Tools/FlashlightTool")]
public class FlashlightTool : Tool
{
    public override void ToolInteraction(GameObject tool)
    {
        Debug.Log("Flashlight Interaction");

        if (tool.activeSelf)
            tool.SetActive(false);
        else
            tool.SetActive(true);
    }
}
