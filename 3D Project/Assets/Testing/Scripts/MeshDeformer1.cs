using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer1 : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices, displacedVertices;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        displacedVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            displacedVertices[i] = vertices[i];
    }

    private void Update()
    {
        return;
        for (int i = 0; i < displacedVertices.Length; i++)
            UpdateVertex(i);
        RecalculateMesh();
    }

    void UpdateVertex(int index)
    {
        Vector3 displacement = displacedVertices[index] - vertices[index];
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.DrawLine(Camera.main.transform.position, point);

        for (int i = 0; i < displacedVertices.Length; i++)
            AddDisplacement(i, point, force);
        RecalculateMesh();
    }

    private void AddDisplacement(int index, Vector3 point, float force)
    {
        Vector3 pointToVertex = mesh.vertices[index] - point;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        displacedVertices[index] = pointToVertex.normalized * velocity;
    }

    void RecalculateMesh()
    {
        mesh.vertices = displacedVertices;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
}
