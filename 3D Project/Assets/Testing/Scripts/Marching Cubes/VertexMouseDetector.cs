using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexMouseDetector : MonoBehaviour
{
    public int index;
    public SingleMarchingCube marchingCube;

    private void OnMouseDown()
    {
        Debug.Log("Clicked on : " + index);
        marchingCube.ChangePoint(index);
    }
}
