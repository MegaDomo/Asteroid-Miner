using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootControls : MonoBehaviour
{
    [Header("Unity References")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    public InputAction shootAction;

    private void Awake()
    {
        shootAction.performed += ctx => { Shoot(ctx); };
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }
}
