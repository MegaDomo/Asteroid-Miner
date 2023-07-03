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
    public float radius = 2;
    public float deformationStrength = 2f;

    private Mesh mesh;
    private Transform playerCam;
    private Vector3[] verticies, modifiedVerts;

    private void Start()
    {
        playerCam = playerManager.GetPlayerCamera().transform;

        mesh = GetComponent<MeshFilter>().mesh;
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
                Vector3 point = interactHit.point;
                Debug.DrawLine(playerCam.position, point);
                Debug.Log("Hitting: " + name);

                for (int v = 0; v < modifiedVerts.Length; v++)
                {
                    Vector3 distance = modifiedVerts[v] - point;

                    Debug.Log(distance.sqrMagnitude);
                    if (distance.sqrMagnitude < radius)
                    {
                        float smoothingFactor = 2f;
                        float force = deformationStrength / (1f + point.sqrMagnitude);

                        if (Input.GetMouseButton(0))
                            modifiedVerts[v] = modifiedVerts[v] + (playerCam.forward * force) / smoothingFactor;
                    }
                }
            }
        }
        

        RecalculateMesh();
    }
}
