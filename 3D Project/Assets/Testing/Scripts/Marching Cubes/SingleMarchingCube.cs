using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMarchingCube : MonoBehaviour
{
    public int isoLevel = 0;

    int[] pointValues;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    public MeshFilter meshFilter;
    Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        CreateSquare();
    }

    void CreateSquare()
    {
        pointValues = new int[8];

        for (int i = 0; i < pointValues.Length; i++)
            pointValues[i] = 1;
    }

    public void ChangePoint(int index)
    {
        pointValues[index] += -2;
        UpdateCube();
    }

    public void UpdateCube()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        int cubeIndex = 0;
        for (int i = 0; i < 8; i++) {
            if (pointValues[i] < isoLevel) {
                cubeIndex |= 1 << i;
            }
        }

        int[] triangulation = MarchingCubesTables.triTable[cubeIndex];

        for (int i = 0; triangulation[i] != -1; i++)
        {
            int indexA = MarchingCubesTables.edgeConnections[triangulation[i]][0];
            int indexB = MarchingCubesTables.edgeConnections[triangulation[i]][1];

            // Find midpoint of Edge
            //Vector3 vertexPos = (MarchingCubesTables.cubeCorners[indexA] + MarchingCubesTables.cubeCorners[indexB]) / 2;

            // Interpolates
            Vector3 vertexPos = Interp(MarchingCubesTables.cubeCorners[indexA], pointValues[indexA],
                               MarchingCubesTables.cubeCorners[indexB], pointValues[indexB]);

            vertices.Add(vertexPos);

            triangles.Add(i);
        }

        UpdateMesh();
    }

    Vector3 Interp(Vector3 vertex1, float valueAtVertex1, Vector3 vertex2, float valueAtVertex2)
    {
        return vertex1 + (isoLevel - valueAtVertex1) * (vertex2 - vertex1) / (valueAtVertex2 - valueAtVertex1);
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
        if (pointValues == null || pointValues.Length == 0)
        {
            return;
        }
        int gridSize = 2;
        int index = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int z = 0; z < gridSize; z++, index++)
                {
                    Gizmos.color = Color.Lerp(Color.black, Color.white, pointValues[index]);
                    Gizmos.DrawSphere(new Vector3(x, y, z), .1f);
                }
            }
        }
    }
}
