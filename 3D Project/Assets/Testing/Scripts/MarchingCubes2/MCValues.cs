using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MCValues 
{
    public static void AddSphereValues(MCGrid grid, Vector3 origin, float radius)
    {
        int gridSize = grid.GetGridSize();
        Vector3 gridCenter = new Vector3((((float)gridSize / 2) - .5f),
                                         (((float)gridSize / 2) - .5f),
                                         (((float)gridSize / 2) - .5f))
                                         + origin;

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float value = Mathf.Abs((gridCenter - new Vector3(x, y, z)).magnitude) - radius;

                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }

    public static void AddSphereValuesWithNoise(MCGrid grid, Vector3 origin, float radius, float noiseScale)
    {
        int gridSize = grid.GetGridSize();
        Vector3 gridCenter = new Vector3((((float)gridSize / 2) - .5f),
                                         (((float)gridSize / 2) - .5f),
                                         (((float)gridSize / 2) - .5f))
                                         + origin;

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float value = Mathf.Abs((gridCenter - new Vector3(x, y, z)).magnitude) - radius;
                    value += Random.Range(-1f * noiseScale, 1f * noiseScale);
                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }
}
