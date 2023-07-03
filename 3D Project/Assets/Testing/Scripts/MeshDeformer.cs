using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    [Header("Attributes")]
    //public float memorySpringForce = 20f;
    public float effectRadius = 2f;
    public float forwardForce = 10f;
    public float damping = 1f;

    private Mesh mesh;
    private Vector3[] vertices, displacedVertices, vertexVelocities;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        displacedVertices = new Vector3[vertices.Length];
        vertexVelocities = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            displacedVertices[i] = vertices[i];
    }

    private void Update()
    {
        for (int i = 0; i < displacedVertices.Length; i++)
            UpdateVertex(i);
        RecalculateMesh();
    }

    void UpdateVertex(int index)
    {
        Vector3 velocity = vertexVelocities[index];
        Vector3 displacement = displacedVertices[index] - vertices[index];
        //velocity -= displacement * memorySpringForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[index] = velocity;
        displacedVertices[index] += velocity * Time.deltaTime;
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        Debug.DrawLine(Camera.main.transform.position, point);

        for (int i = 0; i < displacedVertices.Length; i++)
            AddForce(i, point, force);
    }

    private void AddForce(int index, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[index] - point;

        if (pointToVertex.magnitude < effectRadius)
        {
            float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
            float velocity = attenuatedForce * Time.deltaTime;

            Vector3 forwardForceVector = Camera.main.transform.forward * forwardForce;
            vertexVelocities[index] += (pointToVertex.normalized + forwardForceVector).normalized * velocity;
        }
    }

    void RecalculateMesh()
    {
        mesh.vertices = displacedVertices;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }
}
