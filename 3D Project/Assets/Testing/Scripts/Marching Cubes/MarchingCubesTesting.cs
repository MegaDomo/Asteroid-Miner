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
    public bool useInterp;

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
                    float xCoord = (float)x / gridSize * noiseScale;
                    float yCoord = (float)y / gridSize * noiseScale;
                    float zCoord = (float)z / gridSize * noiseScale;

                    //float noise = Noise.PerlinNoise3D(xCoord, yCoord, zCoord);
                    float noise = 0;
                    pointValues[x, y, z] = useGroundLevel ? ground + noise : noise;
                }
            }
        }
    }

    void AddTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        int triIndex = triangles.Count;
        vertices.Add(a);
        vertices.Add(b);
        vertices.Add(c);
        triangles.Add(triIndex);
        triangles.Add(triIndex + 1);
        triangles.Add(triIndex + 2);
    }

    Vector3 Interp(Vector3 edgeVertext1, float valueAtVertex1, Vector3 edgeVertex2, float valueAtVertex2)
    {
        return edgeVertext1 + (isoLevel - valueAtVertex1) * (edgeVertex2 - edgeVertext1) / (valueAtVertex2 - valueAtVertex1);
    }
/*
    IEnumerator March()
    {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    currentCubePosition = new Vector3(x + .5f, y + .5f, z + .5f);

                    if (useMarchingCubes)
                    {
                        // Set Values at the Corners of the cube
                        float[] cubeValues = new float[] 
                        {
                            pointValues[x, y, z + 1],
                            pointValues[x + 1, y, z + 1],
                            pointValues[x + 1, y, z],
                            pointValues[x, y, z],
                            pointValues[x, y + 1, z + 1],
                            pointValues[x + 1, y + 1, z + 1],
                            pointValues[x + 1, y + 1, z],
                            pointValues[x, y + 1, z],
                        };

                        // Find the Triangluation index
                        int cubeIndex = 0;
                        if (cubeValues[0] < isoLevel) cubeIndex |= 1;
                        if (cubeValues[1] < isoLevel) cubeIndex |= 2;
                        if (cubeValues[2] < isoLevel) cubeIndex |= 4;
                        if (cubeValues[3] < isoLevel) cubeIndex |= 8;
                        if (cubeValues[4] < isoLevel) cubeIndex |= 16;
                        if (cubeValues[5] < isoLevel) cubeIndex |= 32;
                        if (cubeValues[6] < isoLevel) cubeIndex |= 64;
                        if (cubeValues[7] < isoLevel) cubeIndex |= 128;

                        // Get the intersecting edges
                        int[] edges = MarchingCubesTables.triTable[cubeIndex];

                        Vector3 worldPos = new Vector3(x, y, z);

                        int triCount = triangles.Count;

                        // Triangulate
                        for (int i = 0; edges[i] != -1; i +=3)
                        {
                            if (useInterp)
                            {
                                int e00 = MarchingCubesTables.edgeConnections[edges[i]][0];
                                int e01 = MarchingCubesTables.edgeConnections[edges[i]][1];

                                int e10 = MarchingCubesTables.edgeConnections[edges[i + 1]][0];
                                int e11 = MarchingCubesTables.edgeConnections[edges[i + 1]][1];

                                int e20 = MarchingCubesTables.edgeConnections[edges[i + 2]][0];
                                int e21 = MarchingCubesTables.edgeConnections[edges[i + 2]][1];

                            
                                Vector3 a = Interp(MarchingCubesTables.cubeCorners[e00], cubeValues[e00],
                                                   MarchingCubesTables.cubeCorners[e01], cubeValues[e01]) + worldPos;
                                Vector3 b = Interp(MarchingCubesTables.cubeCorners[e10], cubeValues[e10],
                                                   MarchingCubesTables.cubeCorners[e11], cubeValues[e11]) + worldPos;
                                Vector3 c = Interp(MarchingCubesTables.cubeCorners[e20], cubeValues[e20],
                                                   MarchingCubesTables.cubeCorners[e21], cubeValues[e21]) + worldPos;
                                AddTriangle(a, b, c);
                            }
                            else
                            {
                                //AddTriangle(edges[i], edges[i + 1], edges[i + 2]);
                            }

                            
                        }
                    }
                }
            }
        }
    }*/
}
