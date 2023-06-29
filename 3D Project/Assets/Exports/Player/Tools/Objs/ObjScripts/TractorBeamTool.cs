using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TractorBeamTool", menuName = "Tools/TractorBeamTool")]
public class TractorBeamTool : Tool
{
    public override void ToolInteraction()
    {
        Debug.Log("Tractor Beam Toggled");
    }
}
