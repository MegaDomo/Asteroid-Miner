using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowCable : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject ship;

    [Header("Attributes")]
    public float newAsteroidMass;

    private GameObject asteroid;

    private void OnCollisionEnter(Collision collision)
    {
        // This is temporary
        if (collision.gameObject.tag == "Vehicle")
        {
            // creates joint
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            // sets joint posiyion to point of contact
            joint.anchor = collision.contacts[0].point;
            // connects the joint to the other object
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
        }

        if (collision.gameObject.tag == "Asteroid")
        {
            // creates joint
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            // sets joint posiyion to point of contact
            joint.anchor = collision.contacts[0].point;
            // connects the joint to the other object
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;

            collision.gameObject.GetComponent<Asteroid>().LowerMass(newAsteroidMass);
        }
    }
}
