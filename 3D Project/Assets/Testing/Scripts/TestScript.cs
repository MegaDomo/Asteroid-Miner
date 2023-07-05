using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Attributes")]
    public float interactRange = 5f;
    public float radius = 0.01f;
    public float deformationStrength = 0.2f;
    public float smoothingFactor = 2f;

    private Mesh mesh;
    private Transform playerCam;
    private Vector3[] verticies, modifiedVerts;

    private void Start()
    {
        playerCam = playerManager.GetPlayerCamera().transform;

        mesh = GetComponent<MeshFilter>().mesh;
        verticies = mesh.vertices;
        modifiedVerts = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
            modifiedVerts[i] = mesh.vertices[i];
    }

    void RecalculateMesh()
    {
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    private void Update()
    {
        Round1();
    }

    void Round1()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit interactHit,
            interactRange, LayerMask.GetMask("ToolInteractable")))
            {
                Vector3 point = transform.InverseTransformPoint(interactHit.point);
                Debug.DrawLine(playerCam.position, interactHit.point);

                for (int v = 0; v < modifiedVerts.Length; v++)
                {
                    Vector3 distance = modifiedVerts[v] - point;

                    Vector3 newVertPoint = transform.TransformVector(playerCam.forward);
                    Debug.DrawLine(playerCam.position, newVertPoint, Color.green);
                    if (distance.sqrMagnitude < radius)
                    {
                        //float force = deformationStrength / (point.sqrMagnitude);
                        //Vector3 newVertPoint = transform.InverseTransformVector((modifiedVerts[v] + playerCam.forward) * force) ;
                        
                        modifiedVerts[v] = newVertPoint / smoothingFactor;
                    }
                }
            }
        }
        

        RecalculateMesh();
    }
}
