using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Unity References")]
    public MeshFilter meshFilter;

    [Header("Attributes")]
    public float interactRange = 5f;
    public float radius = 2;
    public float deformationStrength = 2f;

    private Mesh mesh;
    private Transform playerCam;
    private Vector3[] verticies, modifiedVerts;

    private void Start()
    {
        playerCam = playerManager.GetPlayer().GetComponent<FirstPersonPlayerMovement>().cam;

        mesh = meshFilter.mesh;
        verticies = mesh.vertices;
        modifiedVerts = mesh.vertices;
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
                for (int v = 0; v < modifiedVerts.Length; v++)
                {
                    Vector3 distance = modifiedVerts[v] - interactHit.point;

                    float smoothingFactor = 2f;
                    float force = deformationStrength / (1f + interactHit.point.sqrMagnitude);

                    if (distance.sqrMagnitude < radius)
                    {
                        Vector3 toOrigin = interactHit.point - transform.position;
                        Debug.Log(toOrigin);
                        if (Input.GetMouseButton(0))
                            modifiedVerts[v] = modifiedVerts[v] + (Vector3.up * force) / smoothingFactor;
                    }
                }
            }
        }
        

        RecalculateMesh();
    }
}
