using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pew : MonoBehaviour
{
    [Header("Attributes")]
    public float shootTime;
    public float pewSpeed;
    public float damage;

    private Rigidbody rb;
    private Vector3 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(dir.normalized * pewSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    public void Seek(Vector3 direction)
    {
        dir = direction;
        Destroy(gameObject, shootTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit " + other.gameObject.name + " for " + damage + " damage");

            Destroy(gameObject);
        }
    }
}
