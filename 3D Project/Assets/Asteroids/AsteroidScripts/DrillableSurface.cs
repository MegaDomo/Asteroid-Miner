using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillableSurface : MonoBehaviour
{
    [Header("Attributes")]
    public float timeToDrill = 2.5f;

    public void CompleteDrilling()
    {
        // Drop Interactables
        Destroy(gameObject);
    }
}
