using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    [Header("Unity References")]
    public Transform cam;

    [Header("Attributes")]
    public float areaOfInfluenceRadius = 4f;
    public float potency = 1f;

    public bool showGizmos;
    Vector3 vec;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit interactHit,
            100f, LayerMask.GetMask("MouseTesting")))
            {
                vec = interactHit.point;
                
                Chunk gen = interactHit.transform.GetComponent<ChunkReference2>().chunk;
                gen.TerraformMesh(vec, areaOfInfluenceRadius, potency);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (showGizmos || vec != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(vec, areaOfInfluenceRadius);
        }
    }
}
