using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCGrid
{
    float[,,] pointValues;

    public float GetValue(int x, int y, int z)
    {
        return pointValues[x, y, z];
    }

    public void SetValue(int x, int y, int z, float value)
    {
        pointValues[x, y, z] = value;
    }
}
