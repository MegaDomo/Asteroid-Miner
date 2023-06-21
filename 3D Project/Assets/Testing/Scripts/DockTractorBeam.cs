using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockTractorBeam : MonoBehaviour
{
    [Header("Unity References")]
    public Transform mainPullPoint;
    public Transform alternatePullPoint;

    [Header("Attributes")]
    public float pullStrength = 1f;
    public List<LayerMask> layersToPullOn;
    public List<string> layersToPullOnStrings;

    private bool isDocked;
    private List<Rigidbody> rbs = new List<Rigidbody>();

    private void OnTriggerEnter(Collider collider)
    {
        foreach (string mask in layersToPullOnStrings)
            if (collider.gameObject.tag == mask)
                rbs.Add(collider.attachedRigidbody);
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

    public bool IsDocked()
    {
        return isDocked;
    }

    private Vector3 CalculatePullPoint()
    {
        if (mainPullPoint == null)
            return alternatePullPoint.position;
        return mainPullPoint.position;
    }
}
