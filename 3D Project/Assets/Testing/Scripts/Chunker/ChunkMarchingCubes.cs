using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMarchingCubes : MonoBehaviour
{
    [Header("Details")]
    public int gridSize = 15;
    public float cellSize = 1f;
    public float isoLevel = 0f;
    public bool useNoise = false;
    public float noiseScale = 1f;
    
    [Header("Debugging")]
    public bool showGizmos = false;

    [Header("Sphere")]
    public float radius;

    Vector3 meshOrigin;
    Vector3 cubeOrigin;

    Mesh mesh;
    MeshFilter meshFilter;
    MCGrid grid;

    List<Vector3> vertices;
    List<int> triangles;

    public void Setup(Vector3 meshOrigin, int gridSize, float cellSize, float isoLevel, float radius, 
                      bool useNoise, float noiseScale, float noiseTransform)
    {
        this.meshOrigin = meshOrigin;
        this.gridSize = gridSize;
        this.cellSize = cellSize;
        this.radius = radius;
        this.isoLevel = isoLevel;
        this.noiseScale = noiseScale;

        cubeOrigin = transform.position;

        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        grid = new MCGrid(gridSize);

        if (useNoise)
            MCValues.AddChunkSphereValuesWithNoise(grid, meshOrigin, cubeOrigin, radius, noiseScale, noiseTransform);
        else
            MCValues.AddChunkSphereValues(grid, meshOrigin, cubeOrigin, radius);
    }

    public void FirstMarch(bool addCollider, bool addRigidBody)
    {
        March();
        AddPhysics(addCollider, addRigidBody);
    }

    void March()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        // We March the number of Boxes which is 1 less than the number of Vertices (GridSize)
        for (int x = 0; x < gridSize - 1; x++)
        {
            for (int y = 0; y < gridSize - 1; y++)
            {
                for (int z = 0; z < gridSize - 1; z++)
                {
                    Vector3 worldPos = new Vector3(x * cellSize, y * cellSize, z * cellSize);

                    // Gets Corners for Box
                    float[] cubeValues = new float[] {
                        grid.GetValue(x    , y    , z + 1),
                        grid.GetValue(x + 1, y    , z + 1),
                        grid.GetValue(x + 1, y    , z    ),
                        grid.GetValue(x    , y    , z    ),
                        grid.GetValue(x    , y + 1, z + 1),
                        grid.GetValue(x + 1, y + 1, z + 1),
                        grid.GetValue(x + 1, y + 1, z    ),
                        grid.GetValue(x    , y + 1, z    ),
                    };

                    // Finds which Permutation
                    int cubeIndex = 0;
                    if (cubeValues[0] > isoLevel) cubeIndex |= 1;
                    if (cubeValues[1] > isoLevel) cubeIndex |= 2;
                    if (cubeValues[2] > isoLevel) cubeIndex |= 4;
                    if (cubeValues[3] > isoLevel) cubeIndex |= 8;
                    if (cubeValues[4] > isoLevel) cubeIndex |= 16;
                    if (cubeValues[5] > isoLevel) cubeIndex |= 32;
                    if (cubeValues[6] > isoLevel) cubeIndex |= 64;
                    if (cubeValues[7] > isoLevel) cubeIndex |= 128;

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
                }
            }
        }
    }

    public void TerraformMesh(Vector3 pointOfInfluence, float areaOfInfluenceRadius, float potency)
    {
        ResetMesh();
        AdjustPointValues(pointOfInfluence, areaOfInfluenceRadius, potency);
        March();
        UpdateColliderMesh();
    }

    void AdjustPointValues(Vector3 pointOfInfluence, float areaOfInfluenceRadius, float potency)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 worldPos = new Vector3(x, y, z) + cubeOrigin;

                    if ((worldPos - pointOfInfluence).magnitude < areaOfInfluenceRadius)
                    {
                        float value = grid.GetValue(x, y, z);
                        grid.SetValue(x, y, z, value + potency);
                    }
                }
            }
        }
    }

    #region Helper Methods
    void AddPhysics(bool addCollider, bool addRigidBody)
    {
        if (meshFilter == null)
            return;

        GameObject obj = meshFilter.gameObject;
        if (addCollider)
        {
            obj.AddComponent<MeshCollider>();
            //obj.GetComponent<MeshCollider>().convex = true;
            obj.layer += 3; 
        }
        if (addRigidBody)
        {
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void UpdateColliderMesh()
    {
        if (mesh == null)
            return;
        meshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    Vector3 Interp(Vector3 vertex1, float valueAtVertex1, Vector3 vertex2, float valueAtVertex2)
    {
        vertex1 *= cellSize;
        vertex2 *= cellSize;
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

        if (GetComponent<MeshCollider>())
            GetComponent<MeshCollider>().sharedMesh = mesh;

    }

    void ResetMesh()
    {
        mesh.Clear();
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Gizmos.color = Color.white;
                    Vector3 vec = meshFilter.transform.position;
                    Gizmos.DrawCube(new Vector3(x * cellSize, y * cellSize, z * cellSize) + vec, Vector3.one * .1f);
                }
            }
        }
    }
}
