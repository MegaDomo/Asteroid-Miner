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

    public float interactRange = 5f;

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
        if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit interactHit,
            interactRange, LayerMask.GetMask("ToolInteractable")))
        {
            Debug.Log("Line44 Hit");
            for (int v = 0; v < modifiedVerts.Length; v++)
            {
                Vector3 distance = modifiedVerts[v] - interactHit.point;
                //Debug.Log("Modified: " + modifiedVerts[v] + "\nHit: " 
                //          + interactHit.point + "\nDistance Vector: " + distance);
                float smoothingFactor = 2f;
                float force = deformationStrength / (1f + interactHit.point.sqrMagnitude);

                if (distance.sqrMagnitude < interactRange)
                {
                    Debug.Log("Line54 passed sqr");
                    Vector3 toOrigin = interactHit.point - transform.position;
                    Debug.Log(toOrigin);
                    if (Input.GetMouseButton(0))
                        modifiedVerts[v] = modifiedVerts[v] + (toOrigin * force) / smoothingFactor;
                }
            }
        }

        RecalculateMesh();
    }
}
