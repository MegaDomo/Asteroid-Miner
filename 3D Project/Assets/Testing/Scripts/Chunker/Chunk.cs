using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData
{
    // Core Details
    public Vector3 gridOrigin;
    public Vector3 gridCenter;
    public Vector3 chunkOrigin;
    public Vector3 gridCoord;

    public int gridSize;
    public float cellSize;
    public float isoLevel;

    // Shape Details
    public float radius;

    // Noise
    public bool useNoise;
    public float noiseScale;
    public float noiseTransform;
}

[System.Serializable]
public class Chunk : MonoBehaviour
{
    [SerializeField] ChunkData data;
    Grid<Chunk> chunks;
    List<Vector3> neighbors;
    Mesh mesh;
    MeshFilter meshFilter;
    MCGrid grid;

    List<Vector3> vertices;
    List<int> triangles;

    Vector3 gridIndex;

    public Chunk()
    {

    }

    private void Start()
    {
        chunks = transform.parent.GetComponent<AsteroidChunkManager>().GetGrid();
        Setup(data, chunks);
    }

    public void Setup(ChunkData data, Grid<Chunk> chunks)
    {
        this.data = data;
        this.chunks = chunks;

        Transform parent = transform.parent;
        data.gridOrigin = parent.position;
        
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        grid = new MCGrid(data.gridSize);

        // ==================================
        // IMPORTANT : Serialize the MCGrid Data, otherwise it is looping through everything
        //             and all the Serialization will be for naught!
        // Likely need to make 2 Setup Methods on for Editor (Serialization) and one for Runtime (Reading Serialized Data)
        // ==================================

        if (data.useNoise)
            MCValues.AddChunkSphereValuesWithNoise(grid, data.gridCenter, data.chunkOrigin, data.radius, data.noiseScale, data.noiseTransform);
        else
            MCValues.AddChunkSphereValues(grid, data.gridCenter, data.chunkOrigin, data.radius);
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
        for (int x = 0; x < data.gridSize - 1; x++) {
            for (int y = 0; y < data.gridSize - 1; y++) {
                for (int z = 0; z < data.gridSize - 1; z++) {
                    Vector3 worldPos = new Vector3(x * data.cellSize, y * data.cellSize, z * data.cellSize);

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
                    float isoLevel = data.isoLevel;
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
        AlertNeighbors(pointOfInfluence, areaOfInfluenceRadius, potency);
        ResetMesh();
        AdjustPointValues(pointOfInfluence, areaOfInfluenceRadius, potency);
        March();
        UpdateColliderMesh();
    }

    private void AlertNeighbors(Vector3 pointOfInfluence, float areaOfInfluenceRadius, float potency)
    {
        int x = (int)data.gridCoord.x;
        int y = (int)data.gridCoord.y;
        int z = (int)data.gridCoord.z;

        //List<Chunk> neighbors = chunks.GetClosestNeighbors(pointOfInfluence, data.gridSize, data.cellSize, x, y, z);
        List<Chunk> neighbors = chunks.GetNeighbors(x, y, z);

        foreach (Chunk chunk in neighbors) {
            //Debug.Log(chunk.data.gridCoord.x + ", " + chunk.data.gridCoord.y + ", " + chunk.data.gridCoord.z);
        }

        Debug.Log(neighbors.Count);
        foreach (Chunk chunk in neighbors)
            chunk.TerraformMeshChild(pointOfInfluence, areaOfInfluenceRadius, potency);
    }

    public void TerraformMeshChild(Vector3 pointOfInfluence, float areaOfInfluenceRadius, float potency)
    {
        ResetMesh();
        AdjustPointValues(pointOfInfluence, areaOfInfluenceRadius, potency);
        March();
        UpdateColliderMesh();
    }
        
    void AdjustPointValues(Vector3 pointOfInfluence, float areaOfInfluenceRadius, float potency)
    {
        Debug.Log(data.chunkOrigin);
        Debug.Log(pointOfInfluence);
        for (int x = 0; x < data.gridSize; x++) {
            for (int y = 0; y < data.gridSize; y++) {
                for (int z = 0; z < data.gridSize; z++) {
                    Vector3 worldPos = new Vector3(x, y, z) + data.chunkOrigin + data.gridOrigin;
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
        vertex1 *= data.cellSize;
        vertex2 *= data.cellSize;
        return vertex1 + (data.isoLevel - valueAtVertex1) * (vertex2 - vertex1) / (valueAtVertex2 - valueAtVertex1);
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

    #region Getters & Setters
    public Vector3 GetGridIndex()
    {
        return gridIndex;
    }

    public void SetNeighbors(List<Vector3> neighbors)
    {
        this.neighbors = neighbors;
    }

    public void SetGridIndex(Vector3 gridIndex)
    {
        this.gridIndex = gridIndex;
    }
    #endregion
}
