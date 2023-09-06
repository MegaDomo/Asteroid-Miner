using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimTheDestroyer : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject target;

    [Header("Attributes")]
    public float speed;
    public float aggroRange;
    public float attackRange;
    public float countDown;
    public float rotationSpeed;

    private float count = 0f;
    private Rigidbody rb;
    private Vector3 randVec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(target.transform.position, transform.position));

        if (Vector3.Distance(target.transform.position, transform.position) >= attackRange 
            && Vector3.Distance(target.transform.position, transform.position) < aggroRange)
        {
            Move();
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > aggroRange)
        {
            if (count <= 0f)
            {
                randVec = GetRandomVec2d();
                count = countDown;
            }
            Patrol(randVec);
            count -= Time.deltaTime;
        }

        if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
        {
            Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
            - transform.position;

            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
            - transform.position;

        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //transform.forward = dir;

        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void Patrol(Vector3 randVec)
    {
        Vector3 dir = transform.position + randVec;

        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //transform.forward = dir;
    }

    private Vector3 GetRandomVec2d()
    {
        return new Vector3(Random.Range(-1f, 1f) * 1000, 0, Random.Range(-1f, 1f) * 1000);
    }

    private Vector3 GetRandomVec3d()
    {
        return new Vector3(Random.Range(-1f, 1f) * 1000, Random.Range(-1f, 1f) * 1000, Random.Range(-1f, 1f) * 1000);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
