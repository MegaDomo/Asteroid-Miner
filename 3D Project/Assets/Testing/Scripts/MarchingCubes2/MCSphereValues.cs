using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSphereValues
{
    MCGrid grid;

    public MCSphereValues(int gridSize, float radius)
    {
        grid = new MCGrid(gridSize);

        Vector3 gridCenter = new Vector3(gridSize / 2, gridSize / 2, gridSize / 2);

        for (int x = 0; x < gridSize - 1; x++) {
            for (int y = 0; y < gridSize - 1; y++) {
                for (int z = 0; z < gridSize - 1; z++) {
                    float value = Mathf.Abs((gridCenter - new Vector3(x, y, z)).magnitude) - radius;

                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }

    public MCGrid GetGrid()
    {
        return grid;
    }
}
