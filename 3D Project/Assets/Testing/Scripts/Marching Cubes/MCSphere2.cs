using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCSphere2 : MonoBehaviour
{
    [Header("Marching Cubers")]
    public int gridSize = 16;
    public float cellSize = 10f;
    public float isoLevel = 0f;
    public MeshFilter meshFilter;

    [Header("Sphere Traits")]
    public float radius;
    public float noiseScale;

    [Header("Display")]
    public bool useMarchDelay;
    public float marchSpeedInSeconds;
    public bool useDebug = false;

    Vector3[,,] grid;
    List<CubeData> cubes;

    List<Vector3> vertices;
    List<int> triangles;

    Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        StartCoroutine("March");
    }

    private void Update()
    {
        March();
        UpdateMesh();
    }

    void March()
    {
        cubes = new List<CubeData>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        Vector3 gridCenter = new Vector3((((float)gridSize / 2) - .5f) * cellSize,
                                         (((float)gridSize / 2) - .5f) * cellSize, 
                                         (((float)gridSize / 2) - .5f) * cellSize)
                                         + transform.position;
        Debug.Log(new Vector3(1 * cellSize, 1 * cellSize, 1 * cellSize));
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {

                    Vector3 worldPos = new Vector3(x * cellSize, y * cellSize, z * cellSize) + transform.position;
                    // Setting the Value and Weight of the Cubes/Corners
                    float[] cubeValues = new float[] {
                        Mathf.Abs((gridCenter - (new Vector3(0f, 0f, 1f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(1f, 0f, 1f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(1f, 0f, 0f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(0f, 0f, 0f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(0f, 1f, 1f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(1f, 1f, 1f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(1f, 1f, 0f) + worldPos)).magnitude) - radius,
                        Mathf.Abs((gridCenter - (new Vector3(0f, 1f, 0f) + worldPos)).magnitude) - radius
                    };
                    CubeData cubeData = new CubeData(new Vector3(x, y, z), cubeValues);
                    cubes.Add(cubeData);

                    if (useDebug)
                    {
                        float stringValue = Mathf.Ceil(cubeValues[0]);
                        Utils.CreateWorldText(new Vector3(x, y, z + 1), stringValue.ToString(), 9, TextAnchor.MiddleCenter);
                    }

                    // Finds which corners are above ground
                    int cubeIndex = 0;
                    if (cubeValues[0] > isoLevel) cubeIndex |= 1;
                    if (cubeValues[1] > isoLevel) cubeIndex |= 2;
                    if (cubeValues[2] > isoLevel) cubeIndex |= 4;
                    if (cubeValues[3] > isoLevel) cubeIndex |= 8;
                    if (cubeValues[4] > isoLevel) cubeIndex |= 16;
                    if (cubeValues[5] > isoLevel) cubeIndex |= 32;
                    if (cubeValues[6] > isoLevel) cubeIndex |= 64;
                    if (cubeValues[7] > isoLevel) cubeIndex |= 128;

                    // Gets the edges
                    int[] edges = MarchingCubesTables.triTable[cubeIndex];

                    int triCount = triangles.Count;

                    for (int i = 0; edges[i] != -1; i +=3) {
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

                    if (triCount != triangles.Count)
                    {
                        mesh.Clear();
                        mesh.SetVertices(vertices);
                        mesh.SetTriangles(triangles, 0);
                        mesh.RecalculateNormals();
                        meshFilter.mesh = mesh;
                    }

                    /*if (useMarchDelay)
                        yield return new WaitForSeconds(marchSpeedInSeconds);*/
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
        if (cubes == null && !useDebug)
            return;

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                for (int z = 0; z < gridSize; z++) {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(new Vector3(x * cellSize, y * cellSize, z * cellSize), Vector3.one * .1f);
                }
            }
        }
    }
}
