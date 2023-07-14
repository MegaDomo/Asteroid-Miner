using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubesSphere : MonoBehaviour
{
    [Header("Grid")]
    public int gridSize = 16;
    public float radius = 4f;
    public int isoLevel = 0;

    [Header("Display")]
    public bool useMarchDelay;
    public float marchSpeedInSeconds = 0.5f;
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
    }

    
    void CreateGrid()
    {
        pointValues = new float[gridSize, gridSize, gridSize];

        // Initialize noise values
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    float noise = Noise.PerlinNoise3D((float)x / gridSize, 
                                                      (float)y / gridSize, 
                                                      (float)z / gridSize);

                    pointValues[x, y, z] = noise;
                }
            }
        }
    }

    IEnumerator March()
    {
        Vector3 gridCenter = new Vector3(gridSize / 2, gridSize / 2, gridSize / 2);

        for (int x = 0; x < gridSize - 1; x++) {
            for (int y = 0; y < gridSize - 1; y++) {
                for (int z = 0; z < gridSize - 1; z++) {
                    currentCubePosition = new Vector3(x + .5f, y + .5f, z + .5f);
                    Vector3 worldPos = new Vector3(x, y, z);

                    // Set values at the corners of the cube
                    float[] cubeValues = new float[] {
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x, y, z + 1).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x + 1, y, z + 1).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x + 1, y, z).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x, y, z).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x, y + 1, z + 1).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x + 1, y + 1, z + 1).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x + 1, y + 1, z).magnitude) - radius,
                        Mathf.Abs(gridCenter.magnitude - new Vector3(x, y + 1, z).magnitude) - radius,
                    };

                    // Find the triangulation index
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

                    int triCount = triangles.Count;

                    // Triangulate
                    for (int i = 0; edges[i] != -1; i += 3)
                    {
                        int e00 = MarchingCubesTables.edgeConnections[edges[i]][0];
                        int e01 = MarchingCubesTables.edgeConnections[edges[i]][1];

                        int e10 = MarchingCubesTables.edgeConnections[edges[i + 1]][0];
                        int e11 = MarchingCubesTables.edgeConnections[edges[i + 1]][1];

                        int e20 = MarchingCubesTables.edgeConnections[edges[i + 2]][0];
                        int e21 = MarchingCubesTables.edgeConnections[edges[i + 2]][1];

                        Vector3 a = Interp(MarchingCubesTables.cubeCorners[e00], cubeValues[e00], MarchingCubesTables.cubeCorners[e01], cubeValues[e01]) + worldPos;
                        Vector3 b = Interp(MarchingCubesTables.cubeCorners[e10], cubeValues[e10], MarchingCubesTables.cubeCorners[e11], cubeValues[e11]) + worldPos;
                        Vector3 c = Interp(MarchingCubesTables.cubeCorners[e20], cubeValues[e20], MarchingCubesTables.cubeCorners[e21], cubeValues[e21]) + worldPos;

                        AddTriangle(a, b, c);
                    }

                    // Only reset mesh when triangles have been added
                    if (triCount != triangles.Count)
                    {
                        mesh.SetVertices(vertices);
                        mesh.SetTriangles(triangles, 0);
                        mesh.RecalculateNormals();
                        meshFilter.mesh = mesh;
                    }

                    if (useMarchDelay)
                    {
                        yield return new WaitForSeconds(marchSpeedInSeconds);
                    }
                }
            }
        }
    }

    Vector3 Interp(Vector3 vertex1, float valueAtVertex1, Vector3 vertex2, float valueAtVertex2)
    {
        return vertex1 + (isoLevel - valueAtVertex1) * (vertex2 - vertex1) / (valueAtVertex2 - valueAtVertex1);
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

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (pointValues == null)
        {
            return;
        }
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Gizmos.color = Color.Lerp(Color.black, Color.white, pointValues[x, y, z]);
                    Gizmos.DrawCube(new Vector3(x, y, z), Vector3.one * .1f);
                }
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(currentCubePosition, Vector3.one);
    }
}
