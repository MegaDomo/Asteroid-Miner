using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScannerTool", menuName = "Tools/ScannerTool")]
public class ScannerTool : Tool
{
    public override void ToolInteraction()
    {
        Debug.Log("Scanner Interaction");
    }
}
