using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    [Header("Unity References")]
    public Transform orbitingPoint;
    public Ellipse orbitPath;

    [Header("Attributes")]
    [Range(0f,1f)]
    public float orbitProgress;
    public float orbitPeriod;
    public bool orbitActive;

    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        if (orbitingPoint)
            SetOrbitingObjectPosition();
    }

    private void Update()
    {
        if (orbitingPoint)
            AnimateOrbit();
    }

    public void AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
            orbitPeriod = 0.1f;
        float orbitSpeed = 1f / orbitPeriod;

        if (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;

            SetOrbitingObjectPosition();
        }
    }

    private void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        float x = orbitPos.x + orbitingPoint.position.x;
        float y = orbitingPoint.position.y;
        float z = orbitPos.y + orbitingPoint.position.z;
        dir = new Vector3(x, y, z);
        transform.localPosition = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!orbitingPoint)
            return;

        orbitActive = false;

        Vector3 normDir = Vector3.Cross(Vector3.up, dir - orbitingPoint.position).normalized;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(normDir * 1000, ForceMode.Impulse);
    }
}
