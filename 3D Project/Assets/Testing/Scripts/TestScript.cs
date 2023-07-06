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
                //Debug.DrawLine(playerCam.position, interactHit.point);
                Debug.DrawLine(playerCam.position, point);

                for (int v = 0; v < modifiedVerts.Length; v++)
                {
                    Vector3 distance = modifiedVerts[v] - point;
                    
                    if (distance.sqrMagnitude < radius )//|| (interactHit.point - transform.position).magnitude > smoothingFactor)
                    {
                        float force = deformationStrength / (point.sqrMagnitude);
                        //Vector3 newVertPoint = transform.InverseTransformVector((modifiedVerts[v] + playerCam.forward) * force) ;

                        //Vector3 newVertPoint = transform.InverseTransformDirection(playerCam.forward);
                        //modifiedVerts[v] = newVertPoint / smoothingFactor;

                        //dir = transform.InverseTransformVector(dir);
                        Vector3 dir = (interactHit.point - transform.position);

                        modifiedVerts[v] += (-dir.normalized * force) / smoothingFactor;
                    }
                }
            }
        }
        

        RecalculateMesh();
    }
}
