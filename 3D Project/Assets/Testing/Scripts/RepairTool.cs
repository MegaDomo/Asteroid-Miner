using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairTool", menuName = "Tools/RepairTool")]
public class RepairTool : Tool
{
    public override void ToolInteraction()
    {
        Debug.Log("RepairTool Interacting");
    }
}
