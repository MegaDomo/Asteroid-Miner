using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrillTool", menuName = "Tools/DrillTool")]
public class DrillTool : Tool
{
    public override void ToolInteraction()
    {
        Debug.Log("DrillTool Interacting");
    }
}
