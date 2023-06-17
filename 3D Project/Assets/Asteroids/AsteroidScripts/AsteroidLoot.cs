using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLoot : MonoBehaviour
{
    [Header("Unity References")]
    public Interaction interaction;
    public Transform player;

    [Header("Attributes")]
    public float floatSpeed;
    public float pickUpRadius;
    public bool willFloatToPlayer;

    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if ((Vector3.Distance(gameObject.transform.position, player.position) < pickUpRadius) && willFloatToPlayer)
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref _velocity, Time.deltaTime * floatSpeed);
    }

    public void PickUpLoot()
    {
        Debug.Log("Loot was picked up");
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            PickUpLoot();
    }

    private void OnEnable()
    {
        interaction.interactAction += PickUpLoot;
    }

    private void OnDisable()
    {
        interaction.interactAction -= PickUpLoot;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
