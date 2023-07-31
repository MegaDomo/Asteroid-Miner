using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    [Header("Attributes")]
    public float areaOfInfluenceRadius = 4f;
    public float potency = 1f;

    bool showGizmos;
    Vector3 vec;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            showGizmos = true;
            vec = Utils.GetMouseWorldPosition();
        }
        else
        {
            showGizmos = false;
        }

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
