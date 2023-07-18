using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData
{
    public Vector3 coord;
    public float[] cubeValues;

    public CubeData(Vector3 coord, float[] cubeValues)
    {
        this.coord = coord;
        this.cubeValues = cubeValues;
    }
}
