using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOff : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Rigidbody rbs = collision.gameObject.GetComponent<Rigidbody>();

            rbs.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
