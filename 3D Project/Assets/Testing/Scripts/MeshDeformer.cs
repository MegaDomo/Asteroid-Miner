using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
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

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.DrawLine(Camera.main.transform.position, point);
    }
}
