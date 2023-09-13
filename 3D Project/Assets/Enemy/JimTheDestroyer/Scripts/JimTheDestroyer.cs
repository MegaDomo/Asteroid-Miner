using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimTheDestroyer : MonoBehaviour
{
    [Header("Unity References")]
    public GameObject target;
    public Transform shootPoint;
    public GameObject pewPrefab;

    [Header("Attributes")]
    public float speed;
    public float aggroRange;
    public float attackRange;
    public float countDown;
    public float rotationSpeed;
    public float attackSpeed;
    public float collisionRange;

    private float count = 0f;
    private float ASCount = 0f;
    private Rigidbody rb;
    private Vector3 randVec;
    private RaycastHit hit;
    private bool avoiding = false;
    private bool right = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(target.transform.position, transform.position));

        if (Vector3.Distance(target.transform.position, transform.position) > attackRange 
            && Vector3.Distance(target.transform.position, transform.position) <= aggroRange)
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

            if (ASCount <= 0f)
            {
                Shoot();
                ASCount = attackSpeed;
            }
            ASCount -= Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
            - transform.position;

        if (Physics.Raycast(transform.position, transform.forward, out hit, collisionRange))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                rb.velocity = rb.velocity / 1.005f;

                transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
                /*if (avoiding == false)
                {
                    avoiding = true;
                    if (LeftOrRight() == 0)
                        right = true;
                    else
                        right = false;
                }

                if (right)
                    transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                else
                    transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);*/
            }
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, collisionRange) ||
            Physics.Raycast(transform.position, transform.forward, out hit, collisionRange))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Force);
        }
        else
        {
            avoiding = false;
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Force);
        }

        //transform.forward = dir;

        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void Patrol(Vector3 randVec)
    {
        Vector3 dir = transform.position + randVec;

        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Force);

        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //transform.forward = dir;
    }

    private void Shoot()
    {
        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
            - transform.position;

        GameObject clone = Instantiate(pewPrefab, shootPoint.position, transform.rotation);
        clone.GetComponent<Pew>().Seek(dir);
    }

    private Vector3 GetRandomVec2d()
    {
        return new Vector3(Random.Range(-1f, 1f) * 1000, 0, Random.Range(-1f, 1f) * 1000);
    }

    private Vector3 GetRandomVec3d()
    {
        return new Vector3(Random.Range(-1f, 1f) * 1000, Random.Range(-1f, 1f) * 1000, Random.Range(-1f, 1f) * 1000);
    }

    private int LeftOrRight()
    {
        return Random.Range(0, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * collisionRange);
        Gizmos.DrawRay(transform.position, transform.right * collisionRange);
        Gizmos.DrawRay(transform.position, transform.right * -collisionRange);
    }
}
