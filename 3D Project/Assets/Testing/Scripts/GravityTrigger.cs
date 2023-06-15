using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<FirstPersonPlayerMovement>()?.SetAsGravityModeMovement();
    }

    public void OnTriggerExit(Collider collider)
    {
        collider.GetComponent<FirstPersonPlayerMovement>()?.SetAsSpaceModeMovement();
    }
}
