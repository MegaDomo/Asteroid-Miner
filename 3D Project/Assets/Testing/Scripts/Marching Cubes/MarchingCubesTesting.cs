using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubesTesting : MonoBehaviour
{
    [Header("Grid")]
    public int gridSize = 16;

    [Header("Marching Cubes")]
    public bool useMarchingCubes;
    public float isoLevel = 0.5f;
    public float noiseScale = 5f;
    public float noiseAmplitude = 5f;
    public bool useGroundLevel;
    [Range(0f, 1f)] public float groundLevel = 0.2f;

    [Header("Display")]
    public bool useMarchDelay;
    public float marchSpeedInSeconds = 0.1f;
    public MeshFilter meshFilter;
    Mesh mesh;

    float[,,] pointValues;

    Vector3 currentCubePosition;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    private void Start()
    {
        mesh = new Mesh();
        CreateGrid();
        StartCoroutine("March");
    }

    void CreateGrid()
    {
        pointValues = new float[gridSize, gridSize, gridSize];

        // Initialize values
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float ground = -y + groundLevel * gridSize;
                    float noise = Noise.PerlinNoise3D((float)x / gridSize * noiseScale,
                                                      (float)y / gridSize * noiseScale,
                                                      (float)z / gridSize * noiseScale);

                    pointValues[x, y, z] = useGroundLevel ? ground + noise : noise;
                }
            }
        }
    }
}
