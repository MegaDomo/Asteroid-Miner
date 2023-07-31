using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCGrid
{
    int gridSize;
    float[,,] pointValues;

    public MCGrid(int gridSize)
    {
        this.gridSize = gridSize;
        pointValues = new float[gridSize, gridSize, gridSize];
    }

    public float GetValue(int x, int y, int z)
    {
        return pointValues[x, y, z];
    }

    public void SetValue(int x, int y, int z, float value)
    {
        pointValues[x, y, z] = value;
    }

    public int GetGridSize()
    {
        return gridSize;
    }
}
