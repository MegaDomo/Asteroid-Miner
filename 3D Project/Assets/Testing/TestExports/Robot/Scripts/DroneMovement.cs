using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [Header("Scriptable References")]
    public PlayerManager playerManager;

    [Header("Attributes")]
    public float flySpeed;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    
}
