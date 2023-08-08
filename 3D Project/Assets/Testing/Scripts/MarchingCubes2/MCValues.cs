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
                    float value = Mathf.Abs((gridCenter - (new Vector3(x, y, z) + origin)).magnitude) - radius;

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
                    float value = Mathf.Abs((gridCenter - (new Vector3(x, y, z) + origin)).magnitude) - radius;
                    value += Random.Range(-1f * noiseScale, 1f * noiseScale);
                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }

    public static void AddChunkSphereValues(MCGrid grid, Vector3 meshOrigin, Vector3 worldPos, float radius)
    {
        int gridSize = grid.GetGridSize();

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float value = Mathf.Abs((meshOrigin - (new Vector3(x, y, z) + worldPos)).magnitude) - radius;
                    
                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }

    public static void AddChunkSphereValuesWithNoise(MCGrid grid, Vector3 meshOrigin, Vector3 worldPos, float radius, 
                                                     float noiseScale, float noiseTransform)
    {
        int gridSize = grid.GetGridSize();

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float value = Mathf.Abs((meshOrigin - (new Vector3(x, y, z) + worldPos)).magnitude) - radius;
                    value += Noise.PerlinNoise3D((x + worldPos.x) * noiseTransform,
                                                 (y + worldPos.y) * noiseTransform, 
                                                 (z + worldPos.z) * noiseTransform) * noiseScale;
                    grid.SetValue(x, y, z, value);
                }
            }
        }
    }
}
