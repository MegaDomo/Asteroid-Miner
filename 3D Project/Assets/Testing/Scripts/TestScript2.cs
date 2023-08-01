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

    bool showGizmos;
    Vector3 vec;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit interactHit,
            100f, LayerMask.GetMask("MouseTesting")))
            {
                vec = interactHit.point;

                MarchingCubesGen gen = interactHit.transform.GetComponent<MCGenReference>().marchingCubesGen;
                gen.TerraformMesh(vec, areaOfInfluenceRadius, potency);
            }

            //vec = Utils.GetMouseWorldPosition();
        }
/*
        if (Input.GetMouseButton(0))
        {
            showGizmos = true;
            vec = Utils.GetMouseWorldPosition();
        }
        else
        {
            showGizmos = false;
        }
*/
    }

    public void OnDrawGizmos()
    {
        if (showGizmos && vec != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(vec, areaOfInfluenceRadius);
        }
    }
}
