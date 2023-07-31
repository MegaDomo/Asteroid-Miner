using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MCValues 
{
    public static void AddSphereValues(MCGrid grid, float radius)
    {
        int gridSize = grid.GetGridSize();
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

    public static void AddSphereValuesWithNoise(MCGrid grid, float radius)
    {
        int gridSize = grid.GetGridSize();
        Vector3 gridCenter = new Vector3(gridSize / 2, gridSize / 2, gridSize / 2);

        for (int x = 0; x < gridSize - 1; x++)
        {
            for (int y = 0; y < gridSize - 1; y++)
            {
                for (int z = 0; z < gridSize - 1; z++)
                {
                    float value = Mathf.Abs((gridCenter - new Vector3(x, y, z)).magnitude) - radius;
                    value += Random.Range(-1f, 1f);
                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }
}
