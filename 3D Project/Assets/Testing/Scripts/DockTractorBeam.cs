using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockTractorBeam : MonoBehaviour
{
    [Header("Unity References")]
    public Transform pullPoint;

    [Header("Attributes")]
    public float pullStrength = 1f;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Vehicle")
            rb = collider.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Vehicle")
        {
            Vector3 dir = pullPoint.position - collider.transform.position;

            // Stops Spasms when on center
            if (dir.magnitude <= 0.1f)
                return;

            rb.AddForce(dir.normalized * pullStrength, ForceMode.Force);
        }
    }
}
