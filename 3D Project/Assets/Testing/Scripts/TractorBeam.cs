using System;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    [Header("Unity References")]
    public Transform mainPullPoint;
    public Transform alternatePullPoint;

    [Header("Attributes")]
    public float pullStrength = 1f;
    //public List<LayerMask> layersToPullOn;
    public List<string> tagsToPullOnStrings;

    private List<bool> objectsAtRest = new List<bool>();
    private List<Rigidbody> rbs = new List<Rigidbody>();

    private void OnTriggerEnter(Collider collider)
    {
        foreach (string tag in tagsToPullOnStrings)
            if (collider.gameObject.tag == tag)
                rbs.Add(collider.attachedRigidbody);

        objectsAtRest.Add(false);
    }

    private void OnTriggerStay(Collider collider)
    {
        foreach (Rigidbody rb in rbs)
        {
            Vector3 dir = CalculatePullPoint() - rb.transform.position;

            // Stops Spasms when on center
            if (dir.magnitude <= 0.1f)
                continue;

            rb.AddForce(dir.normalized * pullStrength, ForceMode.Force);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        rbs.Remove(collider.attachedRigidbody);
    }

    private Vector3 CalculatePullPoint()
    {
        if (mainPullPoint == null)
            return alternatePullPoint.position;
        return mainPullPoint.position;
    }
}
