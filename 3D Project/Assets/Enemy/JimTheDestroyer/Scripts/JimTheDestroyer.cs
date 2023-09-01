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

    private float count = 0f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(target.transform.position, transform.position));

        if (Vector3.Distance(target.transform.position, transform.position) > attackRange 
            && Vector3.Distance(target.transform.position, transform.position) < aggroRange)
        {
            Move();
        }
        else
        {
            Vector3 randVec = GetRandomVec();
            if (count <= 0f)
            {
                randVec = GetRandomVec();
                count = countDown;
            }
            Patrol(randVec);
            count -= Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector3 dir = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
            - transform.position;

        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void Patrol(Vector3 randVec)
    {
        Vector3 dir = transform.position + randVec;

        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
    }

    private Vector3 GetRandomVec()
    {
        return new Vector3(Random.Range(-1f, 1f) * 1000, 0, Random.Range(-1f, 1f) * 1000);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
