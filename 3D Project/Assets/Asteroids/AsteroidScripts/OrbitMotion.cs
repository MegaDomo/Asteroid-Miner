using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    [Header("Unity References")]
    public Transform orbitingObject;
    public Transform orbitingPoint;
    public Ellipse orbitPath;

    [Header("Attributes")]
    [Range(0f,1f)]
    public float orbitProgress;
    public float orbitPeriod;
    public bool orbitActive;

    // Start is called before the first frame update
    void Start()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
        //StartCoroutine(AnimateOrbit());
    }

    private void Update()
    {
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
        orbitingObject.localPosition = new Vector3(x, y, z);
    }

    /*
    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
            orbitPeriod = 0.1f;
        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;

            SetOrbitingObjectPosition();

            yield return null;
        }

    }*/
}
